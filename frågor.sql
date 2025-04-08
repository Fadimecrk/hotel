-- 1. SELECT – Hämta alla rum
SELECT * FROM Rum;

-- 2. SELECT – Hämta alla kunder
SELECT * FROM Kunder;

-- 3. SELECT med JOIN – Visa alla bokningar med kund och rum
SELECT 
    B.Id AS BokningsID,
    K.Namn AS Kund,
    R.Rumsnummer AS Rum,
    B.Incheckning,
    B.Utcheckning
FROM Bokningar B
JOIN Kunder K ON B.KundId = K.Id
JOIN Rum R ON B.RumId = R.Id;

-- 4. WHERE – Hämta bokningar för en viss kund (t.ex. kund med ID 1)
SELECT * FROM Bokningar
WHERE KundId = 1;

-- 5. ORDER BY – Sortera bokningar efter incheckningsdatum (nyaste först)
SELECT * FROM Bokningar
ORDER BY Incheckning DESC;
