namespace Commons.ExternalClients.LkUsers;

public static class LkUserTestData
{
    public const string UsersData = """
                               57baf78d-06e7-44f3-b53d-8d7003f0362f,Майлз Тейлз Прауэр,Майлз,Тейлз,Прауэр,Tails.Prower,Tails.Prower@example.com
                               9db3043e-2e8e-4c9b-8e5e-508704240213,Харлан Эллисон,Харлан,,Эллисон,Harlan.Ellison,Harlan.Ellison@example.com
                               e2594a42-afad-4392-9c6b-7fafd23fb1db,Попов Александр Степанович,Александр,Степанович,Попов,ASPopov,ASPopov@example.com
                               f575018d-d099-465a-9517-2f3fd96b12e9,Артур Чарльз Кларк,Артур,Чарльз,Кларк,Artur.C.Clarke,Artur.C.Clarke@example.com
                               d7fdd06f-e0d7-4858-a93f-d9074082cd3f,Линус Бенедикт Торвальдс,Линус,Бенедикс,Торвальдс,LinusTorvalds,LinusTorvalds@example.com
                               f029574a-78a4-435b-b789-cda4787f1afb,Ричард Мэттью Столлман,Ричард,Мэттью,Столлман,Richard.Stallman,Richard.Stallman@example.com
                               1e56c5b2-7a52-4921-ae21-bd9fc8d3b467,Альберт Эйнштейн,Альберт,,Эйнштейн,Albert.Einstein,Albert.Einstein@example.com
                               4e3aa4b5-87c7-4f64-8b88-438f3c643cde,Никола Тесла,Никола,,Тесла,Nikola.Tesla,Nikola.Tesla@example.com
                               58cc7e3f-d7e9-43c5-be43-838ba47e84a7,Томас Алва Эдисон,Томас,Алва,Эдисон,T.A.Edison,T.A.Edison@example.com
                               20577f6d-280e-49fd-adb6-1785ca7e098c,Уильям Генри Гейтс,Уильям,Гернри,Гейтс,BillGates,BillGates@example.com
                               2aa867c7-2889-478e-adf2-33d41498b3c2,Эдвард Джозеф Сноуден,Эдвард,Джозеф,Сноуден,Snowden,Snowden@example.com
                               b4a9021d-c9b6-43bf-842b-7176e2408584,Гордон Фриман,Гордон,,Фриман,g.freeman,g.freeman@example.com
                               b2d4a5b7-e552-4fa4-a7a0-42c53ab73191,Барни Калхун,Барни,,Калхун,barney.calhoun,barney.calhoun@example.com
                               dabc081f-cc8e-42f2-a96d-42419d1363d2,Гомер Симпсон,Гомер,,Симпсон,h.simpson,h.simpson@example.com
                               242bd078-e9fb-404b-83d1-d6f4ddf6ab49,Гейб Ньюэлл,Гейб,,Ньюэлл,gaben,gaben@example.com
                               e38306f7-d1ce-4b85-83a3-8341da6bd344,Стив Джобс,Стив,,Джобс,S.Jobs,S.Jobs@example.com
                               df3ffdeb-be5d-41a6-bb03-1c4603b20fd9,Стивен Кварц Юнивёрс,Стивен,Кварц,Юнивёрс,Steven.Universe,Steven.Universe@example.com
                               daded057-5556-4e13-ae69-bde74010ffa8,Конни Махесваран,Конни,,Махесваран,K.Maheswaran,K.Maheswaran@example.com
                               dea36e2c-f0df-4fc4-953f-e79e08798a17,Пушкин Александр Сергеевич,Пушкин,Александр,Сергеевич,A.S.Pushkin,A.S.Pushkin@example.com
                               0e2c7f45-15a7-400b-a3ba-36f63dc09434,Достоевский Фёдор Михайлович,Достоевский,Фёдор,Михайлович,F.M.Dostoevsky,F.M.Dostoevsky@example.com
                               80ad1df6-3306-4864-a308-580b53863738,Гоголь Николай Васильевич,Гоголь,Николай,Васильевич,N.V.Gogol,N.V.Gogol@example.com
                               2c48a359-23b7-4e0e-9004-7ae16c9df5ee,Лермонтов Михаил Юрьевич,Лермонтов,Михаил,Юрьевич,Michael.Lermontov,Michael.Lermontov@example.com
                               90288589-da65-4470-be58-2d1e25578ed7,Цветаева Марина Ивановна,Цветаева,Марина,Ивановна,M.I.Tsvetaeva,M.I.Tsvetaeva@example.com
                               0b61e4c4-0b69-427e-ac6c-a7acfeb366f9,Толстой Лев Николаевич,Толстой,Лев,Николаевич,L.N.Tolstoy,L.N.Tolstoy@example.com
                               ee134408-166a-43af-97eb-c3b1daf0cc6b,Петров Павел Петрович,Петров,Павел,Петрович,PPP,PPP@example.com
                               b126d73e-a4d9-4e9b-aff8-8fbd390979b4,Бенджамин Франклин,Бенджамин,,Франклин,B.Franklin,B.Franklin@example.com
                               937fa45f-21d0-430a-bd9a-e72e56b3dad4,Карл Джонсон,Карл,,Джонсон,CJ,CJ@example.com
                               61b179d0-01f7-406c-a0a0-66347d9cacf8,Ада Лавлейс,Ада,,Лавлейс,AdaLovelace,AdaLovelace@example.com
                               e6ab5348-3d0d-4bfb-b67f-7b03c5dbb305,Стивен Кинг,Стивен,,Кинг,Stiven.King,Stiven.King@example.com
                               0b3ae50f-82e4-4369-b9da-ad630cd1ab32,Оскар Уайльд,Оскар,,Уайльд,oskar.wilde,oskar.wilde@example.com
                               0d5221ed-5d7b-46c9-a3ec-c705fb299323,Наоки Йошида,Наоки,,Йошида,Naoki.Yoshida,Naoki.Yoshida@example.com
                               b8665ab4-8d6b-4590-b988-548c3eb3e031,Семёнов Костя Степанович,Костя,Семёнов,Степанович,semyonov.kostya,semyonov.kostya@example.com
                               2ee643ac-0fd8-4412-95b9-c5ba986f4079,Хидео Кодзима,Хидео,,Кодзима,hideokodzima,hideokodzima@example.com
                               f74167d6-abb9-4942-b4cb-0f82fc915575,Юдзи Нака,Юдзи,,Нака,YujiNaka,YujiNaka@example.com
                               d8f72a01-6944-4599-b64b-1efe7b19f2e3,Сигэру Миямото,Сигэру,,Миямото,shigeru.miyamoto,shigeru.miyamoto@example.com
                               16d2374e-1536-4643-a510-d4a0ca0486db,Такаши Иидзука,Такаши,,Иидзука,Takashi.Iizuka,Takashi.Iizuka@example.com
                               50514b80-e050-4156-b277-a4e6c32ff6b8,Аарон Веббер,Аарон,,Веббер,AaronWebber,AaronWebber@example.com
                               bf6961da-bd79-49f4-accf-97ce650b9832,Марк Твен,Марк,,Твен,MarkTwain,MarkTwain@example.com
                               ac544c0b-133b-4ed6-94b0-67b513acc7e9,Томас Сойер,Томас,,Сойер,Tom.Soyer,Tom.Soyer@example.com
                               23a2e3db-cb9a-43da-8c1a-882fd2123c88,О. Генри,О.,,Генри,OGenry,OGenry@example.com
                               999760c8-c6f8-462e-82e2-f238b78c6dd5,Ильф Илья Арнольдович,Илья,Арнольдович,Ильф,I.A.Ilf,I.A.Ilf@example.com
                               c34b054a-a2b9-40c1-95e6-59d61db6816e,Катаев Евгений Петрович,Евгений,Петрович,Катаев,E.P.Kataev,E.P.Kataev@example.com
                               f3f093b9-beeb-4f90-bfe8-f73d11af3976,Остап Сулейман Ибрагим Берта Мария Бендер-бей,Остап Сулейман Ибрагим,Берта Мария,Бендер-бей,OstapBender,OstapBender@example.com
                               2970c6ce-3e03-4229-bb97-067acf69cfc6,Паниковский Михаил Самуэлевич,Михаил,Самуэлевич,Паниковский,Michail.Panikovsky,Michail.Panikovsky@example.com
                               c9a04c21-628a-4cde-bb4b-e8f5aecf77d9,Хаяо Миядзаки,Хаяо,,Миядзаки,HayaoMiyazaki,HayaoMiyazaki@example.com
                               109e46d6-bfdf-4f2c-ab0c-b1076251b168,Кейв Джонсон,Кейв,,Джонсон,cave.johnson,cave.johnson@example.com
                               aa2fb6fc-faa4-4752-81fe-5f887390409c,Филип Джей Фрай,Филип,Джей,Фрай,Philip.Fry,Philip.Fry@example.com
                               1fbca626-4d78-4295-8a9d-6d981adeba13,Рик Санчез,Рик,,Санчез,RickSanchez,RickSanchez@example.com
                               6cba9a98-f042-4c02-9624-43dd0b684121,Эммет Браун,Эммет,,Браун,EmmetBrown,EmmetBrown@example.com
                               022d4d2a-0f77-4f77-9c3d-962e2ef475a4,Марти Макфлай,Марти,,Макфлай,MartinMcfly,MartinMcfly@example.com
                               a69e2b65-8137-4df8-ac38-8467ea04c76e,Энакин Скайуокер,Энакин,,Скайуокер,AnakinSkywalker,AnakinSkywalker@example.com
                               f4081fa2-20a1-4434-b883-13467c99ca50,Люк Скайвокер,Люк,,Скайуокер,LukeSkywalker,LukeSkywalker@example.com
                               ee855aff-9023-4533-be8e-fce7189a627e,Энтони Старк,Энтони,,Старк,Anthony.Stark,Anthony.Stark@example.com
                               601e45db-616f-40b7-9c92-12ace6d96a66,Вольфганг Амадей Моцарт,Вольфганг,Амадей,Моцарт,Mozart,Mozart@example.com
                               cc3aea4d-4c3a-45f9-a098-7ae56fd0b427,Антонио Сальери,Антонио,,Сальери,Salieri,Salieri@example.com
                               bf848d50-d0ff-4b81-8c04-38417bf67a4f,Райан Гослинг,Райан,,Гослинг,RyanGosling,RyanGosling@example.com
                               e1ed6816-416c-4e0d-b1f7-5eca5556c638,Киану Ривс,Киану,,Ривс,Kianu.Reeves,Kianu.Reeves@example.com
                               ec0a388c-e8ff-4b99-ba22-da13b81c6ffd,Гай Юлий Цезарь,Гай,Юлий,Цезарь,Gaius.Caesar,Gaius.Caesar@example.com
                               272c3d5f-e81c-46e6-b2aa-7aef00f6d547,Никколо ди Бернандо Макиавелли,Никколо,ди Бернандо,Макиавелли,Nikkolo.Makiavelli,Nikkolo.Makiavelli@example.com
                               1155e8ce-fb0e-4de5-b2b3-afc9e1ad640e,Оби-ван Кеноби,Оби-ван,,Кеноби,Obi-van.Kenobi,Obi-van.Kenobi@example.com
                               a053545f-1102-4027-8c11-ec693f938197,Квай-гон Джинн,Квай-год,,Джинн,Qui-Gon.Jinn,Qui-Gon.Jinn@example.com
                               e07b585a-e45b-476f-907f-d9f3c65c5ae1,Митра Сурик,Митра,,Сурик,MitraSurik,MitraSurik@example.com
                               4f78ede8-5345-4e81-a7f5-fd5c5e10232d,Ведам Дрен,Ведам,,Дрен,Vedam.Dren,Vedam.Dren@example.com
                               e4436896-462c-45a4-9654-67ca6952f24e,Солид Снейк,Солид,,Снейк,SolidSnake,SolidSnake@example.com
                               7a09e3b4-0c03-401f-ab5d-2b4f9694b481,Ликвид Снейк,Ликвид,,Снейк,LiquidSnake,LiquidSnake@example.com
                               e2c63e76-9b83-4b49-b454-f00ad8331d79,Револьвер Шалашаска Оцелот,Револьвер,Шалашаска,Оцелот,RevolverOcelot,RevolverOcelot@example.com
                               2ca5ce5c-80e4-409d-a3a5-b070badbe723,Майкл Джексон,Майкл,,Джексон,MichaelJackson,MichaelJackson@example.com
                               e93bbb98-25dd-4044-91d4-d7a3a24b5086,Рик Астли,Рик,,Астли,RickAstley,RickAstley@example.com
                               4ce62f65-4c85-4c6e-a6de-74693517e984,Луна Хазбин,Луна,,Хазбин,LoonaHazbin,LoonaHazbin@example.com
                               5a33f686-3aee-4606-a29f-01c4579389f2,Рэйнбоу Дэш,Рэйнбоу,,Дэш,RainbowDash,RainbowDash@example.com
                               2b757de3-5f9c-4ede-b457-3ad6ff0c57c1,Ваш Паникёр,Ваш,,Паникёр,Vash.Paniker,Vash.Paniker@example.com
                               cfa87180-bf5f-4058-b800-4a0282041973,Камина Дзиха,Камина,,Дзиха,Kamina.Giha,Kamina.Giha@example.com
                               0231dda4-506f-4b03-8329-5d4f7a9e3ce8,Шикамару Нара,Шакамуру,,Нара,Shikamaru.Nara,Shikamaru.Nara@example.com
                               bff8b3b0-97a1-44c0-97d3-010fa3acf175,Дэвид Мартинес,Дэвид,,Мартинес,David.Martinez,David.Martinez@example.com
                               23c4ccc7-5ca1-4bc7-a93a-ae96d861f5bd,Кайли Гриффин,Кайли,,Гриффин,Kylie.Griffin,Kylie.Griffin@example.com
                               fba57da9-f40e-42f8-8e7f-f859d3acd804,Джей Ти Марш,Джей,Ти,Марш,JTMarsh,JTMarsh@example.com
                               7730fb28-6c73-4f17-854f-0d6cbfcafc3e,Джейк Клаусон,Джейк,,Клаусон,JakeClawson,JakeClawson@example.com
                               80f1eb4b-5adc-4693-b1d5-eda27962ea90,Тали Зора вас Нормандия,Тали Зора,,вам Нормандия,TaliZora.vasNormandia,TaliZora.vasNormandia@example.com
                               c7fe6269-4270-4500-b6d3-e0b36be9ec92,Джон Шепард,Джон,,Шепард,JohnShepard,JohnShepard@example.com
                               c1ddac03-f2e1-4ed5-8cff-5aedb67e303b,Джайна Праудмур,Джайна,,Праудмур,Jaina.Proudmoore,Jaina.Proudmoore@example.com
                               2d8a3e15-04d6-46ba-832f-617ae6fcc785,Лара Крофт,Лара,,Крофт,LaraCroft,LaraCroft@example.com
                               e7d0d460-6a89-4c07-ba35-d96b5f83983f,Натан Дрейк,Натан,,Дрейк,NatanDrake,NatanDrake@example.com
                               24b08447-9f27-42b3-8818-263f47a4e351,Айзек Кларк,Айзек,,Кларк,Isaac Clarke,Isaac Clarke@example.com
                               18e998ef-619c-4428-bfa5-39fe403e1226,Йоаким Броден,Йоаким,,Броден,Joakim.Broden,Joakim.Broden@example.com
                               0a9f679c-78e6-40af-addd-343efed21dcf,Глен Кук,Глен,,Кук,GlenCook,GlenCook@example.com
                               45713423-4673-4367-9029-a212db7ba36a,Виктор Смольский,Виктор,,Смольский,Viktor.Smolskiy,Viktor.Smolskiy@example.com
                               8a3c98d1-9ded-40ed-845a-738a61ef1a77,Ландо Норрис,Ландо,,Норрис,LandoNorris,LandoNorris@example.com
                               f11755a1-262c-4dcb-9951-72d02f242869,Джон Флойд,Джон,,Флойд,JohnFloyd,JohnFloyd@example.com
                               ebf79968-b962-4c54-9409-f32b3615444b,Осборн Рейнольдс,Осборн,,Рейнольдс,Osborne.Reynolds,Osborne.Reynolds@example.com
                               04884b2c-a5f9-49e5-9f74-521af39a4ae9,Джордж Стокс,Джордж,,Стокс,GeorgeStokes,GeorgeStokes@example.com
                               c5c1f592-c242-4bde-9128-9fa84636680b,Вера Камша,Вера,,Камша,VeraKamsha,VeraKamsha@example.com
                               c507b536-3766-46b8-900a-15b327e94703,Кай Хансен,Кайли,,Хансен,KayHansen,KayHansen@example.com
                               15e99476-c59e-4db1-b92a-63651f247190,Ханси Кёрш,Ханси,,Кёрш,HansyKursh,HansyKursh@example.com
                               2f5b67d3-1970-4921-bd1e-61126aa60aad,Сасс Джордан,Сасс,,Джордан,Sass.Jordan,Sass.Jordan@example.com
                               823c1da3-fb45-4ff2-aa69-d64b7084ecae,Бритни Слэйс,Бритни,,Слейс,BritnieSlays,BritnieSlays@example.com
                               be9d90ef-d294-4b31-a422-d65a825ed6c5,Алисса Уайт-глаз,Алисса,,Уайт-глаз,Alise.White-Gluz,Alise.White-Gluz@example.com
                               eff3d797-bdda-44c8-8335-6e619ca0c6cd,Уолтер Хейзенберг Уайт,Уолтер,Хейзенберг,Уайт,WolterWhite,WolterWhite@example.com
                               5618454f-fc93-41db-915c-55834ad13035,Мэри Шелли,Мэри,,Шелли,MaryShelley,MaryShelley@example.com
                               95bc01ba-2b95-4b2d-819b-92bf5c34f348,Р. Дэниел Оливо,Р. Даниел,,Оливо,R.D.Olivo,R.D.Olivo@example.com
                               ddca3521-f390-45b9-ba9d-f91ef165b676,Айзек Азимов,Айзек,,Азимов,IsaacAsimov,IsaacAsimov@example.com
                               748f1a67-4959-4649-a8f6-30402d4b204b,Вито Андалини,Вито,,Андалини,vitoandolini,vitoandolini@example.com
                               f98027ea-27bb-4dd6-8624-9885d30e4757,Майкл Корлеоне,Майкл,,Корлеоне,michael.corleone,michael.corleone@example.com
                               df5d7030-b80a-4e2c-9336-d923148fc1ae,Генри Джонс Младший,Генри,,Джонс-младший,HenryJohnsJr,HenryJohnsJr@example.com
                               1f680e0f-31e9-446a-ad08-6c2dc59447c7,Дориан Грей,Дориан,,Грей,DorianGray,DorianGray@example.com
                               fb576677-d651-4ad1-9f99-ac895bd32863,Аллан Квотермейн,Аллан,,Квотермейн,AllanQuatermain,AllanQuatermain@example.com
                               70e4e769-0b7c-40c5-a871-33ef419a662d,Абрахам Ван Хельсинг,Абрахам,,Ван Хельсинг,Abraham.Van-Helsing,Abraham.Van-Helsing@example.com
                               63297d3d-12a1-4bcb-aa6a-9f12b63a6747,Джим Хокинс,Джим,,Хокинс,Jim.Hawkins,Jim.Hawkins@example.com
                               30b5fb55-1933-4400-a13b-cb8728ac4a78,Джон Сильвер,Джон,,Сильвер,JonSilver,JonSilver@example.com
                               4e81f954-3cde-4c5a-882b-93c9befc049f,Даниэль Дефо,Жаниэль,,Дефо,DanielDefoe,DanielDefoe@example.com
                               80b9a2a3-295d-40d7-b6b1-cfb4a2ada821,Уильям Дефо,Уильям,,Дефо,WilliamDafoe,WilliamDafoe@example.com
                               bd5c1afb-9dae-48e1-a36d-d01784fbc93a,Робинзон Крузо,Робинзон,,Крузо,RobinsonCrusoe,RobinsonCrusoe@example.com
                               d086a48d-cd48-44a5-bde8-78aa2117deb6,Роберт Льюис Стивенсон,Роберт,Льюис,Стивенсон,Robert.Stivenson,Robert.Stivenson@example.com
                               167bd46f-dc8c-4472-9b83-94d32fb875c9,Пол Бак-и-Рита,Пол,,Бак-и-Рита,Paul.Bach-y-Rita,Paul.Bach-y-Rita@example.com
                               654051e9-510b-4764-b9d5-5050816079ac,Энди Дюфрейн,Энди,,Дюфрейн,Andy.Dufrain,Andy.Dufrain@example.com
                               0914e837-6a4d-4275-8a4e-2d2e21265493,Христофор Колумб,Христофор,,Колумб,Christopher.Columbus,Christopher.Columbus@example.com
                               64e1553f-72da-45ae-bf0c-5af8964efaa4,Артас Менетил,Артас,,Менетил,Artas.Menetil,Artas.Menetil@example.com
                               3171420c-842d-4040-952e-d93e9139663a,Андуин Ринн,Андуин,,Ринн,Anduin.Rinn,Anduin.Rinn@example.com
                               f16731a3-166b-4440-ac16-d7328c88cb49,Илья Ильич Обломов,Илья,Ильич,Обломов,I.I.Oblomov,I.I.Oblomov@example.com
                               374a16fa-9b59-4e9d-bcbc-ced2688a2519,Дуэйн Скала Джонсон,Дуэйн,Скала,Джонс,Dwayne.Johnson,Dwayne.Johnson@example.com
                               b97dc659-dd64-475f-ad8d-3915212d42c2,Ляпис Лазурит,Ляпис,,Лазурит,LapisLazuli,LapisLazuli@example.com
                               a344b7db-1294-46b7-bab5-66d9f6b553ff,Жак Фреско,Жак,,Фреско,Jacque.Fresco,Jacque.Fresco@example.com
                               d812b452-f7e7-44c6-ae94-3461d5b662a0,Правдин Феодох Возражевич,Феодох,Возражевич,Правдин,PhenixWright,PhenixWright@example.com
                               15db1aff-64cc-4d8d-a60f-4e0eba203ac1,Эми Роуз,Эми,,Роуз,AmyRose,AmyRose@example.com
                               """;

    public const string EmployeesData = """
                                        57baf78d-06e7-44f3-b53d-8d7003f0362f,Tails.Prower,Основное место работы,Ведущий инженер-программист,АУП;Административно-обслуживающий персонал,6444a815-3a63-4f2d-9f98-a0873950a9ae,Отдел Корпоративных Информационных Систем,Майлз Тейлз Прауэр
                                        9db3043e-2e8e-4c9b-8e5e-508704240213,Harlan.Ellison,Основное место работы,Главный редактор,АУП;Административно-обслуживающий персонал,1cb5c71f-6abc-4a78-bbd3-8fbeb3be29b0,Издательско-полиграфический центр,Харлан Эллисон
                                        e2594a42-afad-4392-9c6b-7fafd23fb1db,ASPopov,Основное место работы,Ведущий научный сотрудник,НИЧ;Научно-исследовательская часть,90f9d2fb-77b5-4b45-a7eb-4f60ca1a8e2e,Отдел радиоволн,Попов Александр Степанович
                                        f575018d-d099-465a-9517-2f3fd96b12e9,Artur.C.Clarke,Основное место работы,Главный редактор,АУП;Административно-обслуживающий персонал,1cb5c71f-6abc-4a78-bbd3-8fbeb3be29b0,Издательско-полиграфический центр,Артур Чарльз Кларк
                                        d7fdd06f-e0d7-4858-a93f-d9074082cd3f,LinusTorvalds,Основное место работы,Ведущий инженер-программист,АУП;Административно-обслуживающий персонал,2db590a4-7699-41a7-9904-5dab78e967cb,Отдел Открытого Программного Обеспечения,Линус Бенедикт Торвальдс
                                        f029574a-78a4-435b-b789-cda4787f1afb,Richard.Stallman,Основное место работы,Руководитель отдела,АУП;Административно-обслуживающий персонал,2db590a4-7699-41a7-9904-5dab78e967cb,Отдел Открытого Программного Обеспечения,Ричард Мэттью Столлман
                                        2aa867c7-2889-478e-adf2-33d41498b3c2,Snowden,Основное место работы,Руководитель отдела,АУП;Административно-обслуживающий персонал,79d3fa8f-f42b-488d-b8a2-b63d2c084940,Управление безопасности,Эдвард Джозеф Сноуден
                                        b4a9021d-c9b6-43bf-842b-7176e2408584,g.freeman,Внутреннее совместительство,Старший преподаватель,ППС;Профессорско-преподавательский состав,dd9aadb4-227f-405b-bdf0-f3a99e846e71,Кафедра физики,Гордон Фриман
                                        b4a9021d-c9b6-43bf-842b-7176e2408584,g.freeman,Основное место работы,Доцент,НИЧ;Научно-исследовательская часть,dd9aadb4-227f-405b-bdf0-f3a99e846e71,Кафедра физики,Гордон Фриман
                                        b2d4a5b7-e552-4fa4-a7a0-42c53ab73191,barney.calhoun,Основное место работы,Исполнительный продюссер,АУП;Административно-обслуживающий персонал,79d3fa8f-f42b-488d-b8a2-b63d2c084940,Управление безопасности,Барни Калхун
                                        dabc081f-cc8e-42f2-a96d-42419d1363d2,h.simpson,Основное место работы,Инспектор по технике безопасности,АУП;Административно-обслуживающий персонал,dd9aadb4-227f-405b-bdf0-f3a99e846e71,Кафедра физики,Гомер Симпсон
                                        242bd078-e9fb-404b-83d1-d6f4ddf6ab49,gaben,Основное место работы,Руководитель НИИ,АУП;Административно-обслуживающий персонал,5a781de1-4ec3-4bcf-8d5a-b415316e4299,Кафедра игростроя,Гейб Ньюэлл
                                        e38306f7-d1ce-4b85-83a3-8341da6bd344,S.Jobs,Основное место работы,Главный дизайнер,АУП;Административно-обслуживающий персонал,2db590a4-7699-41a7-9904-5dab78e967cb,Отдел Открытого Программного Обеспечения,Стив Джобс
                                        df3ffdeb-be5d-41a6-bb03-1c4603b20fd9,Steven.Universe,Основное место работы,Доцент Доктор психологических наук,НИЧ;Научно-исследовательская часть,1ad416b3-59f0-490d-9822-6dea103ed146,Кафедра психологии,Стивен Кварц Юнивёрс
                                        daded057-5556-4e13-ae69-bde74010ffa8,K.Maheswaran,Основное место работы,"Доцент, кандидат психологических наук",НИЧ;Научно-исследовательская часть,1ad416b3-59f0-490d-9822-6dea103ed146,Кафедра психологии,Конни Махесваран
                                        ec0a388c-e8ff-4b99-ba22-da13b81c6ffd,Gaius.Caesar,Основное место работы,"Доцент, кандидат исторических наук",АУП;Административно-обслуживающий персонал,d3707436-a4b7-41e3-96ba-340e985a1d96,Кафедра мировой истории,Гай Юлий Цезарь
                                        ec0a388c-e8ff-4b99-ba22-da13b81c6ffd,Gaius.Caesar,Внутреннее совместительство,Стратег-тактик,ВОЕН,99c949ac-812c-4181-9916-973fdea34f92,Военный отдел,Гай Юлий Цезарь
                                        ec0a388c-e8ff-4b99-ba22-da13b81c6ffd,Gaius.Caesar,Внутреннее совместительство,Шеф-повар,АУП;Административно-обслуживающий персонал,084f71fc-4fa1-4ddb-a04b-539737cfd074,Комбинат питания,Гай Юлий Цезарь
                                        30b5fb55-1933-4400-a13b-cb8728ac4a78,JonSilver,Внешнее совместительство,Шеф-повар,АУП;Административно-обслуживающий персонал,084f71fc-4fa1-4ddb-a04b-539737cfd074,Комбинат питания,Джон Сильвер
                                        4e81f954-3cde-4c5a-882b-93c9befc049f,DanielDefoe,Основное место работы,Редактор,УВП;Управленческо-вспомогательный персонал,1cb5c71f-6abc-4a78-bbd3-8fbeb3be29b0,Издательско-полиграфический центр,Даниэль Дефо
                                        80b9a2a3-295d-40d7-b6b1-cfb4a2ada821,WilliamDafoe,Основное место работы,Режиссёр-постановщик,АУП;Административно-обслуживающий персонал,5bdaae63-67a3-46c2-88fb-d7b87bfa486b,Театральный отдел,Уильям Дефо
                                        bd5c1afb-9dae-48e1-a36d-d01784fbc93a,RobinsonCrusoe,Основное место работы,Научный сотрудник,НИЧ ПОП,d3707436-a4b7-41e3-96ba-340e985a1d96,Кафедра мировой истории,Робинзон Крузо
                                        d086a48d-cd48-44a5-bde8-78aa2117deb6,Robert.Stivenson,Внешнее совместительство,Рецензент,АУП;Административно-обслуживающий персонал,1cb5c71f-6abc-4a78-bbd3-8fbeb3be29b0,Издательско-полиграфический центр,Роберт Льюис Стивенсон
                                        167bd46f-dc8c-4472-9b83-94d32fb875c9,Paul.Bach-y-Rita,Основное место работы,Преподаватель,Педсостав,61a011ba-c507-45d3-b13e-45d539116226,Кафедра медицины,Пол Бак-и-Рита
                                        654051e9-510b-4764-b9d5-5050816079ac,Andy.Dufrain,Основное место работы,Финансовый директор,АУП;Административно-обслуживающий персонал,9485c91a-8adb-47ec-b97e-2ba9c3d9ac20,Управление финансов,Энди Дюфрейн
                                        0914e837-6a4d-4275-8a4e-2d2e21265493,Christopher.Columbus,Основное место работы,Мастер корабельного дела,ППС;Профессорско-преподавательский состав,d3707436-a4b7-41e3-96ba-340e985a1d96,Кафедра мировой истории,Христофор Колумб
                                        64e1553f-72da-45ae-bf0c-5af8964efaa4,Artas.Menetil,Основное место работы,Бухгалтер,АУП;Административно-обслуживающий персонал,9485c91a-8adb-47ec-b97e-2ba9c3d9ac20,Управление финансов,Артас Менетил
                                        3171420c-842d-4040-952e-d93e9139663a,Anduin.Rinn,Основное место работы,Генератор отчётов,УВП;Управленческо-вспомогательный персонал,9485c91a-8adb-47ec-b97e-2ba9c3d9ac20,Управление финансов,Андуин Ринн
                                        f16731a3-166b-4440-ac16-d7328c88cb49,I.I.Oblomov,Внешнее совместительство,"Доцент, кандидат психологических наук",НИЧ;Научно-исследовательская часть,1ad416b3-59f0-490d-9822-6dea103ed146,Кафедра психологии,Илья Ильич Обломов
                                        374a16fa-9b59-4e9d-bcbc-ced2688a2519,Dwayne.Johnson,Основное место работы,Скальный бровец,ВОЕН,d3707436-a4b7-41e3-96ba-340e985a1d96,Кафедра мировой истории,Дуэйн Скала Джонсон
                                        b97dc659-dd64-475f-ad8d-3915212d42c2,LapisLazuli,Основное место работы,"Доцент, кандидат психологических наук",НИЧ;Научно-исследовательская часть,1ad416b3-59f0-490d-9822-6dea103ed146,Кафедра психологии,Ляпис Лазурит
                                        a344b7db-1294-46b7-bab5-66d9f6b553ff,Jacque.Fresco,Основное место работы,Главный изобретатель-инженер,АУП;Административно-обслуживающий персонал,0f21b057-b952-4ae7-8804-55f16a944238,Инженерный корпус,Жак Фреско
                                        a344b7db-1294-46b7-bab5-66d9f6b553ff,Jacque.Fresco,Внутреннее совместительство,Доцент,НИЧ АУП,a281993e-8b7e-429f-9d86-60db3cb877b1,Кафедра социальных проблем,Жак Фреско
                                        f11755a1-262c-4dcb-9951-72d02f242869,JohnFloyd,Основное место работы,Политолог,УВП;Управленческо-вспомогательный персонал,d3707436-a4b7-41e3-96ba-340e985a1d96,Кафедра мировой истории,Джон Флойд
                                        04884b2c-a5f9-49e5-9f74-521af39a4ae9,GeorgeStokes,Основное место работы,Физик-теоретик,Педсостав,0f21b057-b952-4ae7-8804-55f16a944238,Инженерный корпус,Джордж Стокс
                                        c507b536-3766-46b8-900a-15b327e94703,KayHansen,Основное место работы,Гитарист,НИЧ;Научно-исследовательская часть,5bdaae63-67a3-46c2-88fb-d7b87bfa486b,Театральный отдел,Кай Хансен
                                        15e99476-c59e-4db1-b92a-63651f247190,HansyKursh,Основное место работы,Вокалист,НИЧ АУП,5bdaae63-67a3-46c2-88fb-d7b87bfa486b,Театральный отдел,Ханси Кёрш
                                        be9d90ef-d294-4b31-a422-d65a825ed6c5,Alise.White-Gluz,Основное место работы,Вокалист,НИЧ;Научно-исследовательская часть,5bdaae63-67a3-46c2-88fb-d7b87bfa486b,Театральный отдел,Алисса Уайт-глаз
                                        eff3d797-bdda-44c8-8335-6e619ca0c6cd,WolterWhite,Внешнее совместительство,Консультант,ПОП;Прочий обслуживающий персонал,61a011ba-c507-45d3-b13e-45d539116226,Кафедра медицины,Уолтер Хейзенберг Уайт
                                        5618454f-fc93-41db-915c-55834ad13035,MaryShelley,Основное место работы,Заведующая печатью,АУП;Административно-обслуживающий персонал,1cb5c71f-6abc-4a78-bbd3-8fbeb3be29b0,Издательско-полиграфический центр,Мэри Шелли
                                        ddca3521-f390-45b9-ba9d-f91ef165b676,IsaacAsimov,Основное место работы,Писатель-фантаст,НИЧ АУП,1cb5c71f-6abc-4a78-bbd3-8fbeb3be29b0,Издательско-полиграфический центр,Айзек Азимов
                                        df5d7030-b80a-4e2c-9336-d923148fc1ae,HenryJohnsJr,Основное место работы,Археолог,НИЧ ПОП,d3707436-a4b7-41e3-96ba-340e985a1d96,Кафедра мировой истории,Генри Джонс Младший
                                        70e4e769-0b7c-40c5-a871-33ef419a662d,Abraham.Van-Helsing,Основное место работы,Охотник на вампиров,ВОЕН,d3707436-a4b7-41e3-96ba-340e985a1d96,Кафедра мировой истории,Абрахам Ван Хельсинг
                                        fba57da9-f40e-42f8-8e7f-f859d3acd804,JTMarsh,Основное место работы,Командир,ВОЕН,99c949ac-812c-4181-9916-973fdea34f92,Военный отдел,Джей Ти Марш
                                        7730fb28-6c73-4f17-854f-0d6cbfcafc3e,JakeClawson,Основное место работы,Кот быстрого реагирования,ВОЕН,99c949ac-812c-4181-9916-973fdea34f92,Военный отдел,Джейк Клаусон
                                        c1ddac03-f2e1-4ed5-8cff-5aedb67e303b,Jaina.Proudmoore,Основное место работы,Адмирал,ВОЕН,99c949ac-812c-4181-9916-973fdea34f92,Военный отдел,Джайна Праудмур
                                        2d8a3e15-04d6-46ba-832f-617ae6fcc785,LaraCroft,Основное место работы,Археолог,НИЧ АУП,d3707436-a4b7-41e3-96ba-340e985a1d96,Кафедра мировой истории,Лара Крофт
                                        24b08447-9f27-42b3-8818-263f47a4e351,Isaac Clarke,Основное место работы,Писатель-фантаст,АУП;Административно-обслуживающий персонал,1cb5c71f-6abc-4a78-bbd3-8fbeb3be29b0,Издательско-полиграфический центр,Айзек Кларк
                                        18e998ef-619c-4428-bfa5-39fe403e1226,Joakim.Broden,Основное место работы,Вокалист,АУП;Административно-обслуживающий персонал,5bdaae63-67a3-46c2-88fb-d7b87bfa486b,Театральный отдел,Йоаким Броден
                                        0a9f679c-78e6-40af-addd-343efed21dcf,GlenCook,Основное место работы,Писатель,НИЧ ПОП,1cb5c71f-6abc-4a78-bbd3-8fbeb3be29b0,Издательско-полиграфический центр,Глен Кук
                                        45713423-4673-4367-9029-a212db7ba36a,Viktor.Smolskiy,Основное место работы,Преподаватель,Иные НПР,5bdaae63-67a3-46c2-88fb-d7b87bfa486b,Театральный отдел,Виктор Смольский
                                        2ca5ce5c-80e4-409d-a3a5-b070badbe723,MichaelJackson,Основное место работы,Преподаватель,Иные НПР,5bdaae63-67a3-46c2-88fb-d7b87bfa486b,Театральный отдел,Майкл Джексон
                                        e93bbb98-25dd-4044-91d4-d7a3a24b5086,RickAstley,Основное место работы,Звукорежиссёр,УВП;Управленческо-вспомогательный персонал,5bdaae63-67a3-46c2-88fb-d7b87bfa486b,Театральный отдел,Рик Астли
                                        4ce62f65-4c85-4c6e-a6de-74693517e984,LoonaHazbin,Основное место работы,Секретарь,АУП;Административно-обслуживающий персонал,9485c91a-8adb-47ec-b97e-2ba9c3d9ac20,Управление финансов,Луна Хазбин
                                        cfa87180-bf5f-4058-b800-4a0282041973,Kamina.Giha,Основное место работы,Гвоздь программы,НИЧ;Научно-исследовательская часть,d3707436-a4b7-41e3-96ba-340e985a1d96,Кафедра мировой истории,Камина Дзиха
                                        b126d73e-a4d9-4e9b-aff8-8fbd390979b4,B.Franklin,Основное место работы,Идейный лидер,АУП;Административно-обслуживающий персонал,d3707436-a4b7-41e3-96ba-340e985a1d96,Кафедра мировой истории,Бенджамин Франклин
                                        937fa45f-21d0-430a-bd9a-e72e56b3dad4,CJ,Внешнее совместительство,Преподаватель,ПЕД,61a011ba-c507-45d3-b13e-45d539116226,Кафедра медицины,Карл Джонсон
                                        0b3ae50f-82e4-4369-b9da-ad630cd1ab32,oskar.wilde,Основное место работы,Редактор,ПОП;Прочий обслуживающий персонал,1cb5c71f-6abc-4a78-bbd3-8fbeb3be29b0,Издательско-полиграфический центр,Оскар Уайльд
                                        b8665ab4-8d6b-4590-b988-548c3eb3e031,semyonov.kostya,Основное место работы,Программист,ПОП;Прочий обслуживающий персонал,6444a815-3a63-4f2d-9f98-a0873950a9ae,Отдел Корпоративных Информационных Систем,Семёнов Костя Степанович
                                        2ee643ac-0fd8-4412-95b9-c5ba986f4079,hideokodzima,Основное место работы,Гений,АУП;Административно-обслуживающий персонал,5a781de1-4ec3-4bcf-8d5a-b415316e4299,Кафедра игростроя,Хидео Кодзима
                                        bf6961da-bd79-49f4-accf-97ce650b9832,MarkTwain,Основное место работы,Иностранный писатель,УВП;Управленческо-вспомогательный персонал,1cb5c71f-6abc-4a78-bbd3-8fbeb3be29b0,Издательско-полиграфический центр,Марк Твен
                                        23a2e3db-cb9a-43da-8c1a-882fd2123c88,OGenry,Основное место работы,Писатель,ПОП;Прочий обслуживающий персонал,1cb5c71f-6abc-4a78-bbd3-8fbeb3be29b0,Издательско-полиграфический центр,О. Генри
                                        c34b054a-a2b9-40c1-95e6-59d61db6816e,E.P.Kataev,Основное место работы,Сатирик,НИЧ;Научно-исследовательская часть,5bdaae63-67a3-46c2-88fb-d7b87bfa486b,Театральный отдел,Катаев Евгений Петрович
                                        109e46d6-bfdf-4f2c-ab0c-b1076251b168,cave.johnson,Основное место работы,Руководитель научных исследований,АУП;Административно-обслуживающий персонал,dd9aadb4-227f-405b-bdf0-f3a99e846e71,Кафедра физики,Кейв Джонсон
                                        1fbca626-4d78-4295-8a9d-6d981adeba13,RickSanchez,Основное место работы,Научный исследователь,НИЧ АУП,dd9aadb4-227f-405b-bdf0-f3a99e846e71,Кафедра физики,Рик Санчез
                                        6cba9a98-f042-4c02-9624-43dd0b684121,EmmetBrown,Основное место работы,Практический исследователь,НИЧ;Научно-исследовательская часть,0f21b057-b952-4ae7-8804-55f16a944238,Инженерный корпус,Эммет Браун
                                        ee855aff-9023-4533-be8e-fce7189a627e,Anthony.Stark,Основное место работы,Инженер-конструктор,АУП;Административно-обслуживающий персонал,0f21b057-b952-4ae7-8804-55f16a944238,Инженерный корпус,Энтони Старк
                                        272c3d5f-e81c-46e6-b2aa-7aef00f6d547,Nikkolo.Makiavelli,Основное место работы,Политолог,НИЧ ПОП,d3707436-a4b7-41e3-96ba-340e985a1d96,Кафедра мировой истории,Никколо ди Бернандо Макиавелли
                                        1155e8ce-fb0e-4de5-b2b3-afc9e1ad640e,Obi-van.Kenobi,Основное место работы,Преподаватель,Педсостав,a281993e-8b7e-429f-9d86-60db3cb877b1,Кафедра социальных проблем,Оби-ван Кеноби
                                        a053545f-1102-4027-8c11-ec693f938197,Qui-Gon.Jinn,Основное место работы,Преподаватель,Педсостав,a281993e-8b7e-429f-9d86-60db3cb877b1,Кафедра социальных проблем,Квай-гон Джинн
                                        """;

    public static LkUserDto[] GetUsers()
    {
        return UsersData
            .Split(Environment.NewLine)
            .Select(line => line.Split(","))
            .Select(chunks => new LkUserDto(
                chunks[0],
                chunks[5],
                chunks[1],
                chunks[2],
                chunks[3],
                chunks[4],
                chunks[6]))
            .ToArray();
    }

    public static LkEmployeeDto[] GetEmployees()
    {
        return EmployeesData
            .Split(Environment.NewLine)
            .Select(line => line.Split(","))
            .Select(chunks => new LkEmployeeDto(
                chunks[0],
                chunks[1],
                chunks[7],
                chunks[5],
                chunks[6],
                chunks[4],
                chunks[3]))
            .ToArray();
    }
}