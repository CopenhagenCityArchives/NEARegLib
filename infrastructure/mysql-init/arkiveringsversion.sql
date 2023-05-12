-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Vært: serv09.powerhosting.dk
-- Genereringstid: 08. 05 2023 kl. 08:03:14
-- Serverversion: 10.4.25-MariaDB-1:10.4.25+maria~stretch
-- PHP-version: 8.1.16

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `neaweb`
--

-- --------------------------------------------------------

--
-- Struktur-dump for tabellen `arkiveringsversion`
--

CREATE TABLE `arkiveringsversion` (
  `sys_ID` int(3) NOT NULL,
  `kommune_ID` int(3) NOT NULL,
  `gl_kommune` int(3) NOT NULL,
  `afd_forv` varchar(200) COLLATE latin1_danish_ci NOT NULL,
  `arkiv_nr` varchar(32) COLLATE latin1_danish_ci NOT NULL,
  `parent_arkiv_nr` varchar(32) COLLATE latin1_danish_ci NOT NULL,
  `periode_FRA` date NOT NULL,
  `periode_TIL` date NOT NULL,
  `status` int(2) NOT NULL,
  `test_status` int(3) DEFAULT 0,
  `type` varchar(5) COLLATE latin1_danish_ci NOT NULL,
  `afl_dato` date NOT NULL,
  `mod_dato` date NOT NULL,
  `god_dato` date NOT NULL,
  `afl_naeste_gang` int(2) DEFAULT 0,
  `afl_frekvens` int(2) DEFAULT 0,
  `note` text COLLATE latin1_danish_ci NOT NULL,
  `KK_KMD_NEA` varchar(3) COLLATE latin1_danish_ci NOT NULL,
  `eDoc_nr` varchar(50) COLLATE latin1_danish_ci NOT NULL,
  `medie_mark` varchar(50) COLLATE latin1_danish_ci NOT NULL,
  `user_ID` int(3) NOT NULL,
  `ID` int(4) NOT NULL,
  `medie_type` int(2) DEFAULT 0,
  `av_stoerrelse` float NOT NULL,
  `av_filmaengde` int(10) NOT NULL,
  `av_mappemaengde` int(10) NOT NULL,
  `udleveret` varchar(1) COLLATE latin1_danish_ci NOT NULL DEFAULT '0',
  `udlev_dato` date NOT NULL,
  `kryp_noegle` text COLLATE latin1_danish_ci DEFAULT NULL,
  `type_2` varchar(50) COLLATE latin1_danish_ci NOT NULL,
  `drift_ID` int(4) NOT NULL DEFAULT 0,
  `ksa_prod_ID` int(4) NOT NULL,
  `ksa_prod_YORN` char(1) COLLATE latin1_danish_ci NOT NULL,
  `ksa_prod_timer` varchar(10) COLLATE latin1_danish_ci DEFAULT NULL,
  `ksa_prod_deadline` date DEFAULT NULL,
  `ksa_prod_afventer` varchar(255) COLLATE latin1_danish_ci DEFAULT NULL,
  `ksa_prod_produktionsstatus` varchar(255) COLLATE latin1_danish_ci DEFAULT NULL,
  `ksa_prod_systemsammendrag` text COLLATE latin1_danish_ci DEFAULT NULL,
  `ksa_prod_bemaerkninger` text COLLATE latin1_danish_ci DEFAULT NULL,
  `ksa_prod_faerdig` date DEFAULT NULL,
  `institution` varchar(250) COLLATE latin1_danish_ci NOT NULL,
  `dato_oprettelse` timestamp NOT NULL DEFAULT current_timestamp(),
  `minea_genrejst` char(1) COLLATE latin1_danish_ci NOT NULL DEFAULT 'n',
  `minea_soeg` int(11) NOT NULL DEFAULT 0,
  `minea_note` varchar(250) COLLATE latin1_danish_ci DEFAULT NULL,
  `overfoert_filarkiv` char(1) COLLATE latin1_danish_ci NOT NULL DEFAULT 'n',
  `overfoert_filarkiv_tid` timestamp NULL DEFAULT NULL,
  `overfoert_ziparkiv` char(1) COLLATE latin1_danish_ci NOT NULL DEFAULT 'n',
  `overfoert_ziparkiv_tid` timestamp NULL DEFAULT NULL,
  `overfoert_sb` char(1) COLLATE latin1_danish_ci NOT NULL DEFAULT 'n',
  `overfoert_sb_tid` timestamp NULL DEFAULT NULL,
  `overfoert_burn` char(1) COLLATE latin1_danish_ci NOT NULL DEFAULT 'n',
  `reg_starbas` char(1) COLLATE latin1_danish_ci NOT NULL DEFAULT 'n',
  `reg_starbas_link` varchar(255) COLLATE latin1_danish_ci DEFAULT NULL,
  `sti` text COLLATE latin1_danish_ci DEFAULT NULL,
  `bekendtgoerelse` int(11) NOT NULL,
  `antal_zip_pakker` int(11) DEFAULT NULL,
  `zip_program` int(2) DEFAULT 0,
  `fil_backup_disk_nr` int(2) DEFAULT NULL,
  `filarkiv` int(1) NOT NULL,
  `genbraendes` int(11) NOT NULL DEFAULT 0
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_danish_ci;

--
-- Data dump for tabellen `arkiveringsversion`
--

INSERT INTO `arkiveringsversion` (`sys_ID`,`kommune_ID`,`gl_kommune`,`afd_forv`,`arkiv_nr`,`parent_arkiv_nr`,`periode_FRA`,`periode_TIL`,`status`,`test_status`,`type`,`afl_dato`,`mod_dato`,`god_dato`,`afl_naeste_gang`,`afl_frekvens`,`note`,`KK_KMD_NEA`,`eDoc_nr`,`medie_mark`,`user_ID`,`ID`,`medie_type`,`av_stoerrelse`,`av_filmaengde`,`av_mappemaengde`,`udleveret`,`udlev_dato`,`kryp_noegle`,`type_2`,`drift_ID`,`ksa_prod_ID`,`ksa_prod_YORN`,`ksa_prod_timer`,`ksa_prod_deadline`,`ksa_prod_afventer`,`ksa_prod_produktionsstatus`,`ksa_prod_systemsammendrag`,`ksa_prod_bemaerkninger`,`ksa_prod_faerdig`,`institution`,`dato_oprettelse`,`minea_genrejst`,`minea_soeg`,`minea_note`,`overfoert_filarkiv`,`overfoert_filarkiv_tid`,`overfoert_ziparkiv`,`overfoert_ziparkiv_tid`,`overfoert_sb`,`overfoert_sb_tid`,`overfoert_burn`,`reg_starbas`,`reg_starbas_link`,`sti`,`bekendtgoerelse`,`antal_zip_pakker`,`zip_program`,`fil_backup_disk_nr`,`filarkiv`,`genbraendes`) VALUES (1,42,0,'','40640','10590','2002-10-25','2005-06-01',15,1,'3','0000-00-00','0000-00-00','2007-01-01',0,0,'','KMD','','Boks 6',2,1,1,0.06,39,14,'1','2011-08-23','','',0,0,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'','0000-00-00 00:00:00','n',0,NULL,'y',NULL,'y','2022-12-19 20:31:32','y','2023-01-23 09:23:11','y','n',NULL,NULL,1,2,7,1,6,0);
INSERT INTO `arkiveringsversion` (`sys_ID`,`kommune_ID`,`gl_kommune`,`afd_forv`,`arkiv_nr`,`parent_arkiv_nr`,`periode_FRA`,`periode_TIL`,`status`,`test_status`,`type`,`afl_dato`,`mod_dato`,`god_dato`,`afl_naeste_gang`,`afl_frekvens`,`note`,`KK_KMD_NEA`,`eDoc_nr`,`medie_mark`,`user_ID`,`ID`,`medie_type`,`av_stoerrelse`,`av_filmaengde`,`av_mappemaengde`,`udleveret`,`udlev_dato`,`kryp_noegle`,`type_2`,`drift_ID`,`ksa_prod_ID`,`ksa_prod_YORN`,`ksa_prod_timer`,`ksa_prod_deadline`,`ksa_prod_afventer`,`ksa_prod_produktionsstatus`,`ksa_prod_systemsammendrag`,`ksa_prod_bemaerkninger`,`ksa_prod_faerdig`,`institution`,`dato_oprettelse`,`minea_genrejst`,`minea_soeg`,`minea_note`,`overfoert_filarkiv`,`overfoert_filarkiv_tid`,`overfoert_ziparkiv`,`overfoert_ziparkiv_tid`,`overfoert_sb`,`overfoert_sb_tid`,`overfoert_burn`,`reg_starbas`,`reg_starbas_link`,`sti`,`bekendtgoerelse`,`antal_zip_pakker`,`zip_program`,`fil_backup_disk_nr`,`filarkiv`,`genbraendes`) VALUES (2,37,0,'SUF','AVID.KSA.1','','1982-01-01','2011-10-26',13,10,'false','0000-00-00','0000-00-00','2012-08-20',0,0,'<p>BOKS 8</p>','KK','2011-143988','KK & NEA - Samlet',66,2,3,23.3,100000,53,'0','0000-00-00','encrupt','false',1416,47,'j',NULL,'2012-09-01',NULL,NULL,NULL,NULL,'2012-08-20','','0000-00-00 00:00:00','y',10,NULL,'y',NULL,'y','2022-12-07 13:40:26','y','2023-01-09 13:15:45','y','j',NULL,NULL,2,10,7,4,1,0);

-- Begrænsninger for dumpede tabeller
--

--
-- Indeks for tabel `arkiveringsversion`
--
ALTER TABLE `arkiveringsversion`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `arkiv_nr` (`arkiv_nr`),
  ADD KEY `arkiv_nr_2` (`arkiv_nr`);

--
-- Brug ikke AUTO_INCREMENT for slettede tabeller
--

--
-- Tilføj AUTO_INCREMENT i tabel `arkiveringsversion`
--
ALTER TABLE `arkiveringsversion`
  MODIFY `ID` int(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3321;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
