# Практическая работа №6 — Создание автоматизированных Unit-тестов

## Описание

Проект демонстрирует модульное тестирование класса `BankAccount` методом **"белого ящика"** с использованием MSTest в Visual Studio (.NET Framework 4.7.2).

---

## Структура решения

```
Bank.sln
├── Bank/
│   ├── Bank.csproj          (.NET Framework 4.7.2, Exe)
│   └── BankAccount.cs       (тестируемый класс с XML-документацией)
└── BankTests/
    ├── BankTests.csproj     (.NET Framework 4.7.2, тестовый проект)
    └── BankAccountTests.cs  (8 модульных тестов)
```

---

## Инструкция по открытию в Visual Studio

1. Откройте **Visual Studio 2019/2022**
2. Выберите **Открыть проект или решение**
3. Укажите файл `Bank.sln`
4. В меню **Тест → Запустить все тесты** (или `Ctrl+R, A`)

<img width="641" height="298" alt="image" src="https://github.com/user-attachments/assets/9c11829e-c6ad-4471-9441-a327f44b687e" />
Результат работы программы.

---

## Класс BankAccount — описание методов

| Метод | Описание |
|-------|----------|
| `BankAccount(name, balance)` | Конструктор. Задаёт имя владельца и начальный баланс |
| `Debit(amount)` | Снимает средства со счёта. Бросает `ArgumentOutOfRangeException` если `amount > balance` или `amount < 0` |
| `Credit(amount)` | Зачисляет средства на счёт. Бросает `ArgumentOutOfRangeException` если `amount < 0` |

### XML-документация (фрагмент)

```csharp
/// <summary>
/// Снимает денежные средства со счёта (дебет).
/// </summary>
/// <param name="amount">Сумма для снятия. Должна быть больше 0 и не превышать баланс.</param>
/// <exception cref="ArgumentOutOfRangeException">
/// Выбрасывается, если amount превышает баланс или меньше нуля.
/// </exception>
public void Debit(double amount) { ... }
```

---

## Таблица модульных тестов

| № | Метод теста | Что проверяется | Ожидаемый результат |
|---|-------------|-----------------|---------------------|
| 1 | `Debit_WithValidAmount_UpdatesBalance` | Списание допустимой суммы (4.55 с баланса 11.99) | Баланс = 7.44 ✅ |
| 2 | `Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange` | Списание отрицательной суммы (-100) | `ArgumentOutOfRangeException` с сообщением "Debit amount is less than zero" ✅ |
| 3 | `Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange` | Списание суммы сверх баланса (20 при балансе 11.99) | `ArgumentOutOfRangeException` с сообщением "Debit amount exceeds balance" ✅ |
| 4 | `Credit_WithValidAmount_UpdatesBalance` | Зачисление допустимой суммы (5.77 на баланс 11.99) | Баланс = 17.76 ✅ |
| 5 | `Credit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange` | Зачисление отрицательной суммы (-50) | `ArgumentOutOfRangeException` с сообщением "Credit amount is less than zero" ✅ |
| 6 | `Credit_WithZeroAmount_BalanceUnchanged` | Зачисление нуля (граничный случай) | Баланс не изменился ✅ |
| 7 | `Debit_WithZeroAmount_BalanceUnchanged` | Списание нуля (граничный случай) | Баланс не изменился ✅ |
| 8 | `CreditThenDebit_ResultsInCorrectBalance` | Последовательность Credit(5.77) → Debit(11.22) | Баланс = 6.54 ✅ |

---

<img width="872" height="377" alt="image" src="https://github.com/user-attachments/assets/99f647a5-04f1-47e6-b08a-2bb9baba1a4f" />
Результаты тестов.

## Обнаруженная и исправленная ошибка

В исходном коде метода `Debit` строка:
```csharp
m_balance += amount;  // ОШИБКА: сумма прибавлялась вместо вычитания
```
была исправлена на:
```csharp
m_balance -= amount;  // ИСПРАВЛЕНО
```

Тест `Debit_WithValidAmount_UpdatesBalance` **не прошёл** при первом запуске, обнаружив эту ошибку. После исправления все 8 тестов проходят успешно.

---

## Вывод

Модульное тестирование методом "белого ящика" позволило:
- **Обнаружить логическую ошибку** в методе `Debit` (знак операции)
- **Улучшить код** через рефакторинг: добавлены константы сообщений об ошибках, уточнены исключения
- **Покрыть все ветви выполнения**: допустимые значения, граничные случаи (0), недопустимые значения (отрицательные, превышающие баланс)
- Итог: **8 тестов из 8 пройдено** ✅
