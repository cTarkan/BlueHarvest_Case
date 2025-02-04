import React, { useState, useEffect } from 'react';

interface Customer {
  id: number;
  name: string;
  surname: string;
}

interface Account {
  accountId: number;
  balance: number;
  transactions: Transaction[];
}

interface Transaction {
  amount: number;
  timestamp: string;
}

interface CustomerDetails {
  name: string;
  surname: string;
  totalBalance: number;
  accounts: Account[];
}

const CustomerList = () => {
  const [selectedCustomerId, setSelectedCustomerId] = useState<string>('');
  const [customerDetails, setCustomerDetails] = useState<CustomerDetails | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchCustomerDetails = async () => {
    if (!selectedCustomerId) return;

    setLoading(true);
    setError(null);
    try {
      const response = await fetch(`http://localhost:8080/api/customer/${selectedCustomerId}/details`);
      if (!response.ok) {
        throw new Error('Customer not found');
      }
      const data = await response.json();
      setCustomerDetails(data);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to fetch customer details');
      setCustomerDetails(null);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="customer-list">
      <div className="search-section">
        <div className="input-group">
          <label htmlFor="customerId">Enter Customer ID:</label>
          <input
            id="customerId"
            type="number"
            value={selectedCustomerId}
            onChange={(e) => setSelectedCustomerId(e.target.value)}
          />
        </div>
        <button onClick={fetchCustomerDetails}>View Details</button>
      </div>

      {loading && <p>Loading...</p>}
      {error && <p className="error">{error}</p>}
      
      {customerDetails && (
        <div className="customer-details">
          <h3>Customer Details</h3>
          <p>Name: {customerDetails.name} {customerDetails.surname}</p>
          <p>Total Balance: ${customerDetails.totalBalance.toFixed(2)}</p>
          
          <h4>Accounts</h4>
          {customerDetails.accounts.map((account) => (
            <div key={account.accountId} className="account-details">
              <p>Account ID: {account.accountId}</p>
              <p>Balance: ${account.balance.toFixed(2)}</p>
              
              <h5>Transactions</h5>
              {account.transactions.length > 0 ? (
                <ul>
                  {account.transactions.map((transaction, index) => (
                    <li key={index}>
                      Amount: ${transaction.amount.toFixed(2)} - 
                      Date: {new Date(transaction.timestamp).toLocaleDateString()}
                    </li>
                  ))}
                </ul>
              ) : (
                <p>No transactions</p>
              )}
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default CustomerList;