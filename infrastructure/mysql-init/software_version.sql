CREATE TABLE `software_version` (
  `id` int NOT NULL AUTO_INCREMENT,
  `program` varchar(50) NOT NULL,
  `version` varchar(250) NOT NULL,
  `created` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `program_version` (`program`,`version`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_danish_ci;
