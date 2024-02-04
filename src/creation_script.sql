create database alpha3;
use alpha3;
create table kraj(
    id int not null auto_increment,
    nazev varchar(20) not null,
    primary key(id)
);
create table druh_pozemku(
    id int not null auto_increment,
    nazev varchar(20) not null,
    primary key(id)
);
create table zpusob_vyuziti(
    id int not null auto_increment,
    nazev varchar(20) not null,
    primary key(id)
);
create table zpusob_ochrany(
    id int not null auto_increment,
    nazev varchar(20) not null,
    primary key(id)
);

create table vlastnik(
    id int not null auto_increment,
    identifikator varchar(10) not null, -- rodne cislo nebo ICO
    jmeno varchar(20) not null,
    prijmeni varchar(20) not null,
    adresa varchar(20) not null,
    primary key(id)
);
create table okres(
    id int not null auto_increment,
    nazev varchar(20) not null,
    id_kraj int not null,
    foreign key (id_kraj) references kraj(id),
    primary key(id)
);


create table obec(
    id int not null auto_increment,
    nazev varchar(20) not null,
    id_okres int not null,
    foreign key (id_okres) references okres(id),
    primary key(id)
);
SELECT obec.id,obec.nazev,obec.id_okres,
okres.nazev 
FROM obec
JOIN okres ON obec.id_okres = okres.id;

create table katastralni_uzemi(
    id int not null auto_increment,
    nazev varchar(20) not null,
    id_obec int not null,
    foreign key (id_obec) references obec(id),
    primary key(id)
);

create table pozemek(
    id int not null auto_increment,
    parcela varchar(6) not null, -- check (parcela REGEXP '\d+\/\d+'),
    vymera_m2 int not null,
    id_druh int not null,
    id_zpusob_vyuziti int not null,
    id_zpusob_ochrany int not null,
    popis varchar(20) not null,
    id_katastralni_uzemi int not null,
    foreign key (id_katastralni_uzemi) references katastralni_uzemi(id),
    foreign key (id_druh) references druh_pozemku(id),
    foreign key (id_zpusob_vyuziti) references zpusob_vyuziti(id),
    foreign key (id_zpusob_ochrany) references zpusob_ochrany(id),
    primary key(id)
);
create table vlastnictvi(
    id int not null auto_increment,
    zpusob_nabiti varchar(255) not null,
    id_pozemek int not null,
    id_vlastnik int not null,
    podil int not null,
    foreign key (id_pozemek) references pozemek(id),
    foreign key (id_vlastnik) references vlastnik(id),
    primary key(id)
);

create table vecne_bremeno(
    id int not null auto_increment,
    popis varchar(255) not null,
    poradi_k datetime not null,
    id_opravneni_k int not null,
    id_opravneni_ve_prospech_osobe int,
    id_opravneni_ve_prospech_nemovitosti int,
    foreign key (id_opravneni_k) references pozemek(id),
    foreign key (id_opravneni_ve_prospech_nemovitosti) references pozemek(id),
    foreign key (id_opravneni_ve_prospech_osobe) references vlastnik(id),
    check ( (id_opravneni_ve_prospech_osobe is null and id_opravneni_ve_prospech_nemovitosti is not null) or (id_opravneni_ve_prospech_osobe is not null and id_opravneni_ve_prospech_nemovitosti is null) ),
    primary key(id)
);
create table plomba(
    id int not null auto_increment,
    cislo_jednaciho_rizeni int not null,
    popis varchar(20) not null,
    datum date not null,
    id_pozemek int not null,
    foreign key (id_pozemek) references pozemek(id),
    primary key(id)
);
INSERT INTO druh_pozemku (nazev) VALUES ('zastavěná plocha');

INSERT INTO zpusob_vyuziti (nazev) VALUES ('bydlení');

INSERT INTO zpusob_ochrany (nazev) VALUES ('přírodní památka');

INSERT INTO vlastnik (identifikator, jmeno, prijmeni, adresa) VALUES ('1234567890', 'Jan', 'Novák', 'Praha 1');

INSERT INTO kraj (nazev) VALUES ('Hlavní město Praha');

INSERT INTO okres (nazev, id_kraj) VALUES ('Praha 1', 1);

INSERT INTO obec (nazev, id_okres) VALUES ('Praha', 1);

INSERT INTO katastralni_uzemi (nazev, id_obec) VALUES ('Praha 1', 1);

INSERT INTO pozemek (parcela, vymera_m2, id_druh, id_zpusob_vyuziti, id_zpusob_ochrany, popis, id_katastralni_uzemi) VALUES ('1/1', 100, 1, 1, 1, 'test', 1);

INSERT INTO vlastnictvi (zpusob_nabiti, id_pozemek, id_vlastnik, podil) VALUES ('koupě', 1, 1, 100);

INSERT INTO vecne_bremeno (popis, poradi_k, id_opravneni_k, id_opravneni_ve_prospech_osobe) VALUES ('test', '2020-01-01', 1, 1);

INSERT INTO plomba (cislo_jednaciho_rizeni, popis, datum, id_pozemek) VALUES (1, 'test', '2020-01-01', 1);

INSERT INTO vlastnik (identifikator, jmeno, prijmeni, adresa) VALUES ('0987654321', 'Petr', 'Novák', 'Praha 1');

INSERT INTO vlastnictvi (zpusob_nabiti, id_pozemek, id_vlastnik, podil) VALUES ('koupě', 1, 2, 0);

DELIMITER //
create procedure list_vlastnictvi(
    identifikator varchar(20),
    nazev_kat_uzemi varchar(20)
)
BEGIN
    select p.parcela, p.vymera_m2, p.popis,
    v.jmeno, v.prijmeni, v.adresa, v.identifikator,
    vl.zpusob_nabiti, vl.podil, 
    k.nazev as katastralni_uzemi,
    o.nazev as obec,
    ok.nazev as okres,
    kr.nazev as kraj,
    dp.nazev as druh_pozemku,
    zv.nazev as zpusob_vyuziti,
    zo.nazev as zpusob_ochrany,
    pl.cislo_jednaciho_rizeni, pl.popis as plomba_popis, pl.datum
    from vlastnictvi vl
            inner join pozemek p on vl.id_pozemek = p.id
            inner join vlastnik v on vl.id_vlastnik = v.id
            inner join katastralni_uzemi k on p.id_katastralni_uzemi = k.id
            inner join obec o on k.id_obec = o.id
            inner join okres ok on o.id_okres = ok.id
            inner join kraj kr on ok.id_kraj = kr.id
            inner join druh_pozemku dp on p.id_druh = dp.id
            inner join zpusob_vyuziti zv on p.id_zpusob_vyuziti = zv.id
            inner join zpusob_ochrany zo on p.id_zpusob_ochrany = zo.id
            inner join plomba pl on p.id = pl.id_pozemek
    where v.identifikator = identifikator and k.nazev = nazev_kat_uzemi;
END //
DELIMITER ;


DELIMITER //
create procedure show_parcela(
    cislo_parcely varchar(20),
    nazev_kat_uzemi varchar(20)
)
BEGIN
    select p.parcela, p.vymera_m2, p.popis,
            k.nazev as katastralni_uzemi,
            o.nazev as obec,
            ok.nazev as okres,
            kr.nazev as kraj,
            dp.nazev as druh_pozemku,
            zv.nazev as zpusob_vyuziti,
            zo.nazev as zpusob_ochrany
    from pozemek p
    INNER JOIN katastralni_uzemi k ON p.id_katastralni_uzemi = k.id
    inner join druh_pozemku dp on p.id_druh = dp.id
    INNER JOIN zpusob_vyuziti zv ON p.id_zpusob_vyuziti = zv.id
    INNER JOIN zpusob_ochrany zo ON p.id_zpusob_ochrany = zo.id
    inner join obec o on k.id_obec = o.id
    inner join okres ok on o.id_okres = ok.id
    inner join kraj kr on ok.id_kraj = kr.id
    where p.id_katastralni_uzemi = (select id from katastralni_uzemi where nazev = nazev_kat_uzemi) and 
          p.parcela = cislo_parcely;
END //
DELIMITER ;



DELIMITER //
create procedure change_podil(
    cislo_parcely varchar(20),
    nazev_kat_uzemi varchar(20),
    identifikator_pridani varchar(20),
    identifikator_odebrani varchar(20),
    podil_diff int
)
BEGIN
    START TRANSACTION;
    UPDATE vlastnictvi
    SET podil = podil + podil_diff
    WHERE id_pozemek = (select id from pozemek where parcela = cislo_parcely and id_katastralni_uzemi = (select id from katastralni_uzemi where nazev = nazev_kat_uzemi)) and
        id_vlastnik = (select id from vlastnik where identifikator = identifikator_pridani);

    UPDATE vlastnictvi
    SET podil = podil - podil_diff
    WHERE id_pozemek = (select id from pozemek where parcela = cislo_parcely and id_katastralni_uzemi = (select id from katastralni_uzemi where nazev = nazev_kat_uzemi)) and
        id_vlastnik = (select id from vlastnik where identifikator = identifikator_odebrani);
    COMMIT;
END //
DELIMITER ;

select * from vlastnictvi
WHERE id_pozemek = (select id from pozemek where parcela = 1/1 and id_katastralni_uzemi = (select id from katastralni_uzemi where nazev = 'Praha 1')) and
    id_vlastnik = (select id from vlastnik where identifikator = '1234567890');;
Call list_vlastnictvi('1234567890', 'Praha 1');
Call show_parcela('1/1', 'Praha 1');
Call change_podil('1/1', 'Praha 1', '0987654321', '1234567890', 10);
drop procedure list_vlastnictvi;
drop procedure show_parcela;
drop procedure change_podil;


SELECT katastralni_uzemi.id,katastralni_uzemi.nazev,katastralni_uzemi.id_obec,
       obec.nazev, obec.id_okres,
       okres.nazev, okres.id_kraj,
       kraj.nazev, kraj.id
FROM katastralni_uzemi
         INNER JOIN obec ON katastralni_uzemi.id_obec = obec.id
         INNER JOIN okres ON obec.id_okres = okres.id
         INNER JOIN kraj ON okres.id_kraj = kraj.id;
;
select * from katastralni_uzemi;