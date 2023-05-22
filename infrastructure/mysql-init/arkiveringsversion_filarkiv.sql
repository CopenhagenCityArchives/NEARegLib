-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Vært: serv09.powerhosting.dk
-- Genereringstid: 22. 05 2023 kl. 11:26:47
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
-- Struktur-dump for tabellen `arkiveringsversion_filarkiv`
--

CREATE TABLE `arkiveringsversion_filarkiv` (
  `id` int(11) NOT NULL,
  `server` varchar(25) COLLATE latin1_danish_ci NOT NULL,
  `ip` varchar(20) COLLATE latin1_danish_ci NOT NULL,
  `netvaerks_navn` varchar(150) COLLATE latin1_danish_ci NOT NULL,
  `kapacitet_TB` decimal(4,1) NOT NULL,
  `kapacitet_GB` decimal(7,1) NOT NULL,
  `laast` varchar(1) COLLATE latin1_danish_ci NOT NULL DEFAULT 'n'
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_danish_ci;

--
-- Data dump for tabellen `arkiveringsversion_filarkiv`
--

INSERT INTO `arkiveringsversion_filarkiv` (`id`, `server`, `ip`, `netvaerks_navn`, `kapacitet_TB`, `kapacitet_GB`, `laast`) VALUES
(3, 'Arkiv3', '172.30.15.162', '\\\\Arkiv3\\filarkiv\\', 120.0, 0.0, 'y'),
(4, 'KSA-ARKIV4', '172.30.15.20', '\\\\KSA-ARKIV4\\filarkiv', 61.9, 63385.6, 'n'),
(5, 'KSA-ARKIV5', '172.30.15.25', '\\\\KSA-ARKIV5\\filarkiv\r\n', 61.9, 63385.6, 'n'),
(6, 'KSA-ARKIV6', '172.30.15.30', '\\\\KSA-ARKIV6\\filarkiv', 61.9, 63385.6, 'n'),
(7, 'KSA-ARKIV7', '172.30.15.31', '\\\\KSA-ARKIV7\\filarkiv', 61.9, 63385.6, 'n'),
(8, 'KSA-ARKIV8', '172.30.15.33', '\\\\KSA-ARKIV8\\filarkiv', 61.9, 63385.6, 'n');

--
-- Begrænsninger for dumpede tabeller
--

--
-- Indeks for tabel `arkiveringsversion_filarkiv`
--
ALTER TABLE `arkiveringsversion_filarkiv`
  ADD PRIMARY KEY (`id`);

--
-- Brug ikke AUTO_INCREMENT for slettede tabeller
--

--
-- Tilføj AUTO_INCREMENT i tabel `arkiveringsversion_filarkiv`
--
ALTER TABLE `arkiveringsversion_filarkiv`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
