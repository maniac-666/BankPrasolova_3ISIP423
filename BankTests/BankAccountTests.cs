using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountNS;

namespace BankTests
{
    /// <summary>
    /// Класс модульных тестов для <see cref="BankAccount"/>.
    /// Тестирует методы Debit и Credit по методу "белого ящика".
    /// </summary>
    [TestClass]
    public class BankAccountTests
    {
        // ===========================
        // ТЕСТЫ МЕТОДА Debit
        // ===========================

        /// <summary>
        /// Тест 1: При допустимой сумме списания баланс корректно уменьшается.
        /// </summary>
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act
            account.Debit(debitAmount);

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }

        /// <summary>
        /// Тест 2: Если сумма списания меньше нуля — должно выбрасываться ArgumentOutOfRangeException.
        /// </summary>
        [TestMethod]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = -100.00;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act
            try
            {
                account.Debit(debitAmount);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message, BankAccount.DebitAmountLessThanZeroMessage);
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        /// <summary>
        /// Тест 3: Если сумма списания превышает баланс — должно выбрасываться ArgumentOutOfRangeException.
        /// </summary>
        [TestMethod]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 20.0;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // Act
            try
            {
                account.Debit(debitAmount);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        // ===========================
        // ТЕСТЫ МЕТОДА Credit
        // ===========================

        /// <summary>
        /// Тест 4: При допустимой сумме зачисления баланс корректно увеличивается.
        /// </summary>
        [TestMethod]
        public void Credit_WithValidAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double creditAmount = 5.77;
            double expected = 17.76;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act
            account.Credit(creditAmount);

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not credited correctly");
        }

        /// <summary>
        /// Тест 5: Если сумма зачисления меньше нуля — должно выбрасываться ArgumentOutOfRangeException.
        /// </summary>
        [TestMethod]
        public void Credit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double creditAmount = -50.00;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act
            try
            {
                account.Credit(creditAmount);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message, "Credit amount is less than zero");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        /// <summary>
        /// Тест 6: Зачисление нуля не изменяет баланс (граничный случай).
        /// </summary>
        [TestMethod]
        public void Credit_WithZeroAmount_BalanceUnchanged()
        {
            // Arrange
            double beginningBalance = 11.99;
            double creditAmount = 0.0;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act
            account.Credit(creditAmount);

            // Assert
            Assert.AreEqual(beginningBalance, account.Balance, 0.001,
                "Balance should not change when crediting zero");
        }

        /// <summary>
        /// Тест 7: Списание нуля не изменяет баланс (граничный случай).
        /// </summary>
        [TestMethod]
        public void Debit_WithZeroAmount_BalanceUnchanged()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 0.0;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act
            account.Debit(debitAmount);

            // Assert
            Assert.AreEqual(beginningBalance, account.Balance, 0.001,
                "Balance should not change when debiting zero");
        }

        /// <summary>
        /// Тест 8: Последовательные операции Credit и Debit дают правильный итоговый баланс.
        /// </summary>
        [TestMethod]
        public void CreditThenDebit_ResultsInCorrectBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act
            account.Credit(5.77);   // 11.99 + 5.77 = 17.76
            account.Debit(11.22);   // 17.76 - 11.22 = 6.54

            // Assert
            Assert.AreEqual(6.54, account.Balance, 0.001,
                "Balance after credit and debit is incorrect");
        }
    }
}
