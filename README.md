# Wallet

1.	Pobiera cyklicznie kursy walut z tabeli B udostępnionej przez NBP i zapisuje je
2.	Pozwala tworzyć i nazywać "portfele wielowalutowe". Portfele mogą być listowane razem z swoją zawartością.
3.	Pozwala wykonywać następujące transakcje: 
    -  Zasilenie portfela dowolną kwotą w dowolnej walucie dostępnej w tabeli B.
    -  Wypłatą z portfela dowolnej kwoty dostępnej w portfelu.
    - Konwersję części zawartości portfela na inną walutę, z wykorzystaniem najnowszych dostępnych kursów (przeliczenie musi odbyć się za pośrednictwem waluty PLN oczywiście).


## Uruchomienie
Aplikację można uruchomić na Dockerze
```powershell
docker compose build
docker compose up
```

Przeglądanie komponentów aplikacji powinno być możliwe przy użyciu poniższych adresów URL:
```
ExchangeRates API : http://localhost:5001/swagger
Wallet API : http://localhost:5002/swagger
```
