import React, { useState } from 'react';

const TransactionForm = () => {
  const [accountId, setAccountId] = useState('');
  const [amount, setAmount] = useState('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const response = await fetch('http://localhost:8080/api/transaction', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          accountId: parseInt(accountId),
          amount: parseFloat(amount),
        }),
      });

      if (response.ok) {
        setAccountId('');
        setAmount('');
        alert('Transaction created successfully!');
      } else {
        const error = await response.text();
        alert(`Failed to create transaction: ${error}`);
      }
    } catch (error) {
      console.error('Error creating transaction:', error);
      alert('Failed to create transaction. Please try again.');
    }
  };

  return (
    <form className="form" onSubmit={handleSubmit}>
      <div className="input-group">
        <label htmlFor="accountId">Account ID:</label>
        <input
          id="accountId"
          type="number"
          value={accountId}
          onChange={(e) => setAccountId(e.target.value)}
          required
        />
      </div>
      <div className="input-group">
        <label htmlFor="amount">Amount:</label>
        <input
          id="amount"
          type="number"
          step="0.01"
          value={amount}
          onChange={(e) => setAmount(e.target.value)}
          required
        />
      </div>
      <button type="submit">Create Transaction</button>
    </form>
  );
};

export default TransactionForm;