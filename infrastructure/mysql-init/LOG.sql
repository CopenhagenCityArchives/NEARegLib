-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Vært: serv09.powerhosting.dk
-- Genereringstid: 08. 05 2023 kl. 08:04:05
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
-- Struktur-dump for tabellen `LOG`
--

CREATE TABLE `LOG` (
  `ID` int(11) NOT NULL,
  `gruppe` int(11) NOT NULL COMMENT '1 = AV, 2 = SFTP, 3 = burnt, 4 = SB, 5 = ZIP, 6 = filarkiv',
  `AVID` int(11) NOT NULL,
  `beskrivelse` varchar(255) NOT NULL,
  `ts` timestamp NULL DEFAULT NULL,
  `user_ID` int(11) NOT NULL,
  `log_fra` timestamp NULL DEFAULT NULL,
  `log_til` timestamp NULL DEFAULT NULL,
  `errors_occurred` tinyint(1) NULL DEFAULT NULL,
  `software_version` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Data dump for tabellen `LOG`
--

INSERT INTO `LOG` (`ID`, `gruppe`, `AVID`, `beskrivelse`, `ts`, `user_ID`, `log_fra`, `log_til`, `errors_occurred`, `software_version`) VALUES
(1, 1, 1, 'Genaflevering - Fejlrettes af DAF', NULL, 19, '2014-06-23 09:06:27', '2014-06-29 09:08:01', NULL, NULL),
(17913, 1, 1, '2.2 Under produktion', '2023-05-03 12:01:22', 191, NULL, NULL, NULL, NULL),
(17914, 2, 1, 'Lukket', '2023-05-08 07:16:16', 4, NULL, NULL, NULL, NULL),
(17915, 5, 2, 'Overført', '2023-05-08 07:19:56', 4, NULL, NULL, NULL, NULL),
(17921, 1, 2, 'Genrejsning', '2023-05-08 07:59:57', 19, NULL, NULL, NULL, NULL);

--
-- Begrænsninger for dumpede tabeller
--

--
-- Indeks for tabel `LOG`
--
ALTER TABLE `LOG`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `errors_occurred` (`errors_occurred`),
  ADD KEY `software_version` (`software_version`);

--
-- Brug ikke AUTO_INCREMENT for slettede tabeller
--

--
-- Tilføj AUTO_INCREMENT i tabel `LOG`
--
ALTER TABLE `LOG`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17922;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
