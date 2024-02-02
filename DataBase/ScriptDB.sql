DROP DATABASE intersectmessages;
CREATE DATABASE intersectmessages;
USE intersectmessages;

CREATE TABLE`satelite`(
`SateliteId` INT NOT NULL AUTO_INCREMENT, 
`SateliteName` VARCHAR(255)  NULL,
`CoordinateX` VARCHAR(255)  NULL,
`CoordinateY` VARCHAR(255)  NULL,
`AuditDate` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
PRIMARY KEY (`SateliteId`) ) ENGINE = InnoDB;

CREATE TABLE `messagesIntersect` (
    `MessageId` INT NOT NULL AUTO_INCREMENT,
    `SateliteIdRef` INT NOT NULL,
    `Consecutive` INT NOT NULL,
    `Distance` DECIMAL(12 , 2) NOT NULL,
    `Message` VARCHAR(2255) NULL,
    `AuditDate` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    PRIMARY KEY (`MessageId`),UNIQUE(MessageId),
    FOREIGN KEY (`SateliteIdRef`) REFERENCES `satelite`(`SateliteId`)
) ENGINE = InnoDB;

CREATE TABLE `decryptedMessage` (
    `decryptedMessageId` INT NOT NULL AUTO_INCREMENT,
    `Consecutive` INT,
    `CoordinateX` DECIMAL  NULL,
	`CoordinateY` DECIMAL  NULL,
    `Message` VARCHAR(2255) NULL,
    `AuditDate` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    PRIMARY KEY (`decryptedMessageId`),UNIQUE(`Consecutive`)
  
) ENGINE = InnoDB;

INSERT INTO `satelite` (`SateliteId`,`SateliteName`,`CoordinateX`,`CoordinateY`,`AuditDate`) VALUES (1,'Kenobi','-500','-200','2023-10-24 22:04:40');
INSERT INTO `satelite` (`SateliteId`,`SateliteName`,`CoordinateX`,`CoordinateY`,`AuditDate`) VALUES (2,'Skywalker','100','-100','2023-10-24 22:04:58');
INSERT INTO `satelite` (`SateliteId`,`SateliteName`,`CoordinateX`,`CoordinateY`,`AuditDate`) VALUES (3,'Sato','500','100','2023-10-24 22:05:26');

