import React, { useState } from 'react';

const AccountForm = () => {
  const [customerId, setCustomerId] = useState('');
  const [initialCredit, setInitialCredit] = useState('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const response = await fetch('http://localhost:8080/api/account', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          customerId: parseInt(customerId),
          initialCredit: parseFloat(initialCredit),
        }),
      });

      if (response.ok) {
        setCustomerId('');
        setInitialCredit('');
        alert('Account created successfully!');
      } else {
        const error = await response.text();
        alert(`Failed to create account: ${error}`);
      }
    } catch (error) {
      console.error('Error creating account:', error);
      alert('Failed to create account. Please try again.');
    }
  };

  return (
    <form className="form" onSubmit={handleSubmit}>
      <div className="input-group">
        <label htmlFor="customerId">Customer ID:</label>
        <input
          id="customerId"
          type="number"
          value={customerId}
          onChange={(e) => setCustomerId(e.target.value)}
          required
        />
      </div>
      <div className="input-group">
        <label htmlFor="initialCredit">Initial Credit:</label>
        <input
          id="initialCredit"
          type="number"
          step="0.01"
          value={initialCredit}
          onChange={(e) => setInitialCredit(e.target.value)}
          required
        />
      </div>
      <button type="submit">Create Account</button>
    </form>
  );
};

export default AccountForm;