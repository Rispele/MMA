#!/bin/bash
set -e

echo "===> Checking kernel modules"
lsmod | grep ppp || echo "⚠️  PPP modules not visible (expected in container)"

echo "echo \"===> Updating ip routes: 10.74.0.0/16 dev ppp0\"" >> /etc/ppp/ip-up
echo "ip route add 10.74.0.0/16 dev ppp0" >> /etc/ppp/ip-up

echo "===> Starting IPsec"
ipsec start
sleep 2
ipsec up l2tp

echo "===> Starting xl2tpd"
mkdir -p /var/run/xl2tpd
touch /var/run/xl2tpd/l2tp-control
touch /var/log/ppp.log

xl2tpd -D &
sleep 2

echo "===> Triggering L2TP tunnel"
echo "c vpn" > /var/run/xl2tpd/l2tp-control

echo "===> Logs"
tail -f /var/log/ppp.log
