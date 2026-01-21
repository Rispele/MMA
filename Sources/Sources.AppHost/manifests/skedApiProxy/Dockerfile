FROM ubuntu:22.04

ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update && apt-get install -y \
    strongswan \
    xl2tpd \
    ppp \
    iproute2 \
    iptables \
    tcpdump \
    curl \
    ca-certificates \
    iputils-ping \
    && apt-get clean

# pppd MUST be suid
RUN chmod u+s /usr/sbin/pppd

# Configs
COPY ipsec.conf /etc/ipsec.conf
COPY ipsec.secrets /etc/ipsec.secrets
COPY xl2tpd.conf /etc/xl2tpd/xl2tpd.conf
COPY options.l2tpd.client /etc/ppp/options.l2tpd.client

# Runtime
COPY entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh

CMD ["/entrypoint.sh"]
