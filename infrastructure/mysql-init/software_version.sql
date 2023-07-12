CREATE TABLE `software_version` (
  `id` int NOT NULL AUTO_INCREMENT,
  `program` varchar(50) NOT NULL,
  `informationalVersion` varchar(250) NOT NULL,
  `created` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `program_informationalVersion` (`program`,`informationalVersion`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_danish_ci;
