# ExchangeRatesUpdater

Код SQL функции, возвращающей курс валюты на определенную дату:

```
CREATE FUNCTION [dbo].[GetExchangeRate] (@date date, @currency varchar(3))
RETURNS real
AS
BEGIN
	DECLARE @result real

	SELECT @result = rate FROM dbo.exchange_rates
	WHERE rates_date = @date AND currency = @currency

	RETURN @result
END;
```

