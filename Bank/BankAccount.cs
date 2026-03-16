using System;

namespace BankAccountNS
{
    /// <summary>
    /// Класс банковского счёта. Демонстрационный пример для модульного тестирования.
    /// </summary>
    public class BankAccount
    {
        /// <summary>
        /// Сообщение об ошибке: сумма списания превышает баланс.
        /// </summary>
        public const string DebitAmountExceedsBalanceMessage = "Debit amount exceeds balance";

        /// <summary>
        /// Сообщение об ошибке: сумма списания меньше нуля.
        /// </summary>
        public const string DebitAmountLessThanZeroMessage = "Debit amount is less than zero";

        private readonly string m_customerName;
        private double m_balance;

        private BankAccount() { }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BankAccount"/>.
        /// </summary>
        /// <param name="customerName">Имя владельца счёта.</param>
        /// <param name="balance">Начальный баланс счёта.</param>
        public BankAccount(string customerName, double balance)
        {
            m_customerName = customerName;
            m_balance = balance;
        }

        /// <summary>
        /// Получает имя владельца счёта.
        /// </summary>
        /// <value>Строка с именем клиента.</value>
        public string CustomerName
        {
            get { return m_customerName; }
        }

        /// <summary>
        /// Получает текущий баланс счёта.
        /// </summary>
        /// <value>Текущий баланс в виде числа с плавающей точкой.</value>
        public double Balance
        {
            get { return m_balance; }
        }

        /// <summary>
        /// Снимает денежные средства со счёта (дебет).
        /// </summary>
        /// <param name="amount">Сумма для снятия. Должна быть больше 0 и не превышать баланс.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Выбрасывается, если <paramref name="amount"/> превышает баланс или меньше нуля.
        /// </exception>
        public void Debit(double amount)
        {
            if (amount > m_balance)
            {
                throw new ArgumentOutOfRangeException("amount", amount, DebitAmountExceedsBalanceMessage);
            }
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount", amount, DebitAmountLessThanZeroMessage);
            }
            m_balance -= amount;
        }

        /// <summary>
        /// Зачисляет денежные средства на счёт (кредит).
        /// </summary>
        /// <param name="amount">Сумма для зачисления. Должна быть больше или равна 0.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Выбрасывается, если <paramref name="amount"/> меньше нуля.
        /// </exception>
        public void Credit(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount", amount, "Credit amount is less than zero");
            }
            m_balance += amount;
        }

        /// <summary>
        /// Точка входа в программу. Демонстрирует работу банковского счёта.
        /// </summary>
        public static void Main()
        {
            BankAccount ba = new BankAccount("Mr. Roman Abramovich", 11.99);
            ba.Credit(5.77);
            ba.Debit(11.22);
            Console.WriteLine("Current balance is ${0}", ba.Balance);
            Console.ReadLine();
        }
    }
}
