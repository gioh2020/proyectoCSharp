DROP DATABASE intersectmessages;
CREATE DATABASE intersectmessages;
USE intersectmessages;
CREATE TABLE`messagesIntersect`(
`MessageId` INT NOT NULL AUTO_INCREMENT, 
`MessageNum` INT NOT NULL ,
`Satelite` VARCHAR(255)  NULL,
`AuditDate` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

PRIMARY KEY (`MessageId`) ) ENGINE = InnoDB;