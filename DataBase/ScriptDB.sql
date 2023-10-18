DROP DATABASE intersectmessages;
CREATE DATABASE intersectmessages;
USE intersectmessages;

CREATE TABLE`satelite`(
`SateliteId` INT NOT NULL AUTO_INCREMENT, 
`SateliteName` VARCHAR(255)  NULL,
`Coordenadas` VARCHAR(255)  NULL,
`Coordenadax` VARCHAR(255)  NULL,
`Coordenaday` VARCHAR(255)  NULL,
`AuditDate` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
PRIMARY KEY (`SateliteId`) ) ENGINE = InnoDB;

CREATE TABLE `messagesIntersect` (
    `MessageId` INT NOT NULL AUTO_INCREMENT,
    `SateliteIdRef` INT,
    `MessageNum` INT NOT NULL,
    `Message` VARCHAR(255) NULL,
    `AuditDate` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    PRIMARY KEY (`MessageId`),
    FOREIGN KEY (`SateliteIdRef`) REFERENCES `satelite`(`SateliteId`)
) ENGINE = InnoDB;

