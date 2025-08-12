CREATE TABLE kullanicilar (
    id INT AUTO_INCREMENT PRIMARY KEY,
    kullanici_adi VARCHAR(50) NOT NULL UNIQUE,
    sifre CHAR(32) NOT NULL
);

INSERT INTO kullanicilar (kullanici_adi, sifre) VALUES
('admin', MD5('12345')),
('kullanıcı', MD5('123'));
