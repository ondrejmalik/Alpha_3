CREATE DATABASE  IF NOT EXISTS `alpha3` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `alpha3`;
-- MySQL dump 10.13  Distrib 8.0.33, for Win64 (x86_64)
--
-- Host: localhost    Database: alpha3
-- ------------------------------------------------------
-- Server version	8.0.33

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `druh_pozemku`
--

DROP TABLE IF EXISTS `druh_pozemku`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `druh_pozemku` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nazev` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `druh_pozemku`
--

LOCK TABLES `druh_pozemku` WRITE;
/*!40000 ALTER TABLE `druh_pozemku` DISABLE KEYS */;
INSERT INTO `druh_pozemku` VALUES (1,'zastavěná plocha');
/*!40000 ALTER TABLE `druh_pozemku` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `katastralni_uzemi`
--

DROP TABLE IF EXISTS `katastralni_uzemi`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `katastralni_uzemi` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nazev` varchar(20) NOT NULL,
  `id_obec` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_obec` (`id_obec`),
  CONSTRAINT `katastralni_uzemi_ibfk_1` FOREIGN KEY (`id_obec`) REFERENCES `obec` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `katastralni_uzemi`
--

LOCK TABLES `katastralni_uzemi` WRITE;
/*!40000 ALTER TABLE `katastralni_uzemi` DISABLE KEYS */;
INSERT INTO `katastralni_uzemi` VALUES (1,'Praha 1',1);
/*!40000 ALTER TABLE `katastralni_uzemi` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `kraj`
--

DROP TABLE IF EXISTS `kraj`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `kraj` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nazev` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `kraj`
--

LOCK TABLES `kraj` WRITE;
/*!40000 ALTER TABLE `kraj` DISABLE KEYS */;
INSERT INTO `kraj` VALUES (1,'Hlavní město Praha');
/*!40000 ALTER TABLE `kraj` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `obec`
--

DROP TABLE IF EXISTS `obec`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `obec` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nazev` varchar(20) NOT NULL,
  `id_okres` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_okres` (`id_okres`),
  CONSTRAINT `obec_ibfk_1` FOREIGN KEY (`id_okres`) REFERENCES `okres` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `obec`
--

LOCK TABLES `obec` WRITE;
/*!40000 ALTER TABLE `obec` DISABLE KEYS */;
INSERT INTO `obec` VALUES (1,'Praha',1);
/*!40000 ALTER TABLE `obec` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `okres`
--

DROP TABLE IF EXISTS `okres`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `okres` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nazev` varchar(20) NOT NULL,
  `id_kraj` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_kraj` (`id_kraj`),
  CONSTRAINT `okres_ibfk_1` FOREIGN KEY (`id_kraj`) REFERENCES `kraj` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `okres`
--

LOCK TABLES `okres` WRITE;
/*!40000 ALTER TABLE `okres` DISABLE KEYS */;
INSERT INTO `okres` VALUES (1,'Praha 1',1);
/*!40000 ALTER TABLE `okres` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `plomba`
--

DROP TABLE IF EXISTS `plomba`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `plomba` (
  `id` int NOT NULL AUTO_INCREMENT,
  `cislo_jednaciho_rizeni` int NOT NULL,
  `popis` varchar(20) NOT NULL,
  `datum` date NOT NULL,
  `id_pozemek` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_pozemek` (`id_pozemek`),
  CONSTRAINT `plomba_ibfk_1` FOREIGN KEY (`id_pozemek`) REFERENCES `pozemek` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `plomba`
--

LOCK TABLES `plomba` WRITE;
/*!40000 ALTER TABLE `plomba` DISABLE KEYS */;
INSERT INTO `plomba` VALUES (1,1,'test','2020-01-01',1);
/*!40000 ALTER TABLE `plomba` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pozemek`
--

DROP TABLE IF EXISTS `pozemek`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pozemek` (
  `id` int NOT NULL AUTO_INCREMENT,
  `parcela` varchar(6) NOT NULL,
  `vymera_m2` int NOT NULL,
  `id_druh` int NOT NULL,
  `id_zpusob_vyuziti` int NOT NULL,
  `id_zpusob_ochrany` int NOT NULL,
  `popis` varchar(20) NOT NULL,
  `id_katastralni_uzemi` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_katastralni_uzemi` (`id_katastralni_uzemi`),
  KEY `id_druh` (`id_druh`),
  KEY `id_zpusob_vyuziti` (`id_zpusob_vyuziti`),
  KEY `id_zpusob_ochrany` (`id_zpusob_ochrany`),
  CONSTRAINT `pozemek_ibfk_1` FOREIGN KEY (`id_katastralni_uzemi`) REFERENCES `katastralni_uzemi` (`id`),
  CONSTRAINT `pozemek_ibfk_2` FOREIGN KEY (`id_druh`) REFERENCES `druh_pozemku` (`id`),
  CONSTRAINT `pozemek_ibfk_3` FOREIGN KEY (`id_zpusob_vyuziti`) REFERENCES `zpusob_vyuziti` (`id`),
  CONSTRAINT `pozemek_ibfk_4` FOREIGN KEY (`id_zpusob_ochrany`) REFERENCES `zpusob_ochrany` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pozemek`
--

LOCK TABLES `pozemek` WRITE;
/*!40000 ALTER TABLE `pozemek` DISABLE KEYS */;
INSERT INTO `pozemek` VALUES (1,'1/1',100,1,1,1,'test',1);
/*!40000 ALTER TABLE `pozemek` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vecne_bremeno`
--

DROP TABLE IF EXISTS `vecne_bremeno`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vecne_bremeno` (
  `id` int NOT NULL AUTO_INCREMENT,
  `popis` varchar(255) NOT NULL,
  `poradi_k` datetime NOT NULL,
  `id_opravneni_k` int NOT NULL,
  `id_opravneni_ve_prospech_osobe` int DEFAULT NULL,
  `id_opravneni_ve_prospech_nemovitosti` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `id_opravneni_k` (`id_opravneni_k`),
  KEY `id_opravneni_ve_prospech_nemovitosti` (`id_opravneni_ve_prospech_nemovitosti`),
  KEY `id_opravneni_ve_prospech_osobe` (`id_opravneni_ve_prospech_osobe`),
  CONSTRAINT `vecne_bremeno_ibfk_1` FOREIGN KEY (`id_opravneni_k`) REFERENCES `pozemek` (`id`),
  CONSTRAINT `vecne_bremeno_ibfk_2` FOREIGN KEY (`id_opravneni_ve_prospech_nemovitosti`) REFERENCES `pozemek` (`id`),
  CONSTRAINT `vecne_bremeno_ibfk_3` FOREIGN KEY (`id_opravneni_ve_prospech_osobe`) REFERENCES `vlastnik` (`id`),
  CONSTRAINT `vecne_bremeno_chk_1` CHECK ((((`id_opravneni_ve_prospech_osobe` is null) and (`id_opravneni_ve_prospech_nemovitosti` is not null)) or ((`id_opravneni_ve_prospech_osobe` is not null) and (`id_opravneni_ve_prospech_nemovitosti` is null))))
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vecne_bremeno`
--

LOCK TABLES `vecne_bremeno` WRITE;
/*!40000 ALTER TABLE `vecne_bremeno` DISABLE KEYS */;
INSERT INTO `vecne_bremeno` VALUES (1,'test','2020-01-01 00:00:00',1,1,NULL);
/*!40000 ALTER TABLE `vecne_bremeno` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vlastnictvi`
--

DROP TABLE IF EXISTS `vlastnictvi`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vlastnictvi` (
  `id` int NOT NULL AUTO_INCREMENT,
  `zpusob_nabiti` varchar(255) NOT NULL,
  `id_pozemek` int NOT NULL,
  `id_vlastnik` int NOT NULL,
  `podil` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_pozemek` (`id_pozemek`),
  KEY `id_vlastnik` (`id_vlastnik`),
  CONSTRAINT `vlastnictvi_ibfk_1` FOREIGN KEY (`id_pozemek`) REFERENCES `pozemek` (`id`),
  CONSTRAINT `vlastnictvi_ibfk_2` FOREIGN KEY (`id_vlastnik`) REFERENCES `vlastnik` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vlastnictvi`
--

LOCK TABLES `vlastnictvi` WRITE;
/*!40000 ALTER TABLE `vlastnictvi` DISABLE KEYS */;
INSERT INTO `vlastnictvi` VALUES (1,'koupě',1,1,90),(2,'koupě',1,2,10);
/*!40000 ALTER TABLE `vlastnictvi` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vlastnik`
--

DROP TABLE IF EXISTS `vlastnik`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vlastnik` (
  `id` int NOT NULL AUTO_INCREMENT,
  `identifikator` varchar(10) NOT NULL,
  `jmeno` varchar(20) NOT NULL,
  `prijmeni` varchar(20) NOT NULL,
  `adresa` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vlastnik`
--

LOCK TABLES `vlastnik` WRITE;
/*!40000 ALTER TABLE `vlastnik` DISABLE KEYS */;
INSERT INTO `vlastnik` VALUES (1,'1234567890','Jan','Novák','Praha 1'),(2,'0987654321','Petr','Novák','Praha 1');
/*!40000 ALTER TABLE `vlastnik` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `zpusob_ochrany`
--

DROP TABLE IF EXISTS `zpusob_ochrany`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `zpusob_ochrany` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nazev` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `zpusob_ochrany`
--

LOCK TABLES `zpusob_ochrany` WRITE;
/*!40000 ALTER TABLE `zpusob_ochrany` DISABLE KEYS */;
INSERT INTO `zpusob_ochrany` VALUES (1,'přírodní památka');
/*!40000 ALTER TABLE `zpusob_ochrany` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `zpusob_vyuziti`
--

DROP TABLE IF EXISTS `zpusob_vyuziti`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `zpusob_vyuziti` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nazev` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `zpusob_vyuziti`
--

LOCK TABLES `zpusob_vyuziti` WRITE;
/*!40000 ALTER TABLE `zpusob_vyuziti` DISABLE KEYS */;
INSERT INTO `zpusob_vyuziti` VALUES (1,'bydlení');
/*!40000 ALTER TABLE `zpusob_vyuziti` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'alpha3'
--

--
-- Dumping routines for database 'alpha3'
--
/*!50003 DROP PROCEDURE IF EXISTS `change_podil` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `change_podil`(
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
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `list_vlastnictvi` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `list_vlastnictvi`(
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
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `show_parcela` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `show_parcela`(
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
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-02-04 20:08:22
