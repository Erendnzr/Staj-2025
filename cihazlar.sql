CREATE TABLE cihazlar (
    id INT AUTO_INCREMENT PRIMARY KEY,
    cihaz_id VARCHAR(50) NOT NULL,
    cihaz_adi VARCHAR(100) NOT NULL
);


INSERT INTO cihazlar (id, cihaz_id, cihaz_adi) VALUES
(1, 'Cihaz1', 'Cihaz1');
