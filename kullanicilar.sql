CREATE TABLE IF NOT EXISTS kullanicilar (
    id INT AUTO_INCREMENT PRIMARY KEY,
    kullanici_adi VARCHAR(50) NOT NULL,
    sifre VARCHAR(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO kullanicilar (kullanici_adi, sifre) VALUES
('admin', MD5('12345'),
('kullanıcı', MD5('123'));
