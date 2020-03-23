--
-- Setup the tech table for the PHP assignment.
--
USE websoft;

DROP TABLE IF EXISTS tech;
CREATE TABLE tech (
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    label CHAR(10),
    type VARCHAR(20)
);

INSERT INTO tech (label, type) VALUES
    ('Apache', 'Web server'),
    ('PHP', 'Server side language'),
    ('MariaDB', 'Database server'),
    ('MySQL', 'Database server')
;
