import React from 'react';
import CustomerForm from './components/Customer/CustomerForm';
import CustomerList from './components/Customer/CustomerList';
import AccountForm from './components/Account/AccountForm';
import TransactionForm from './components/Transaction/TransactionForm';
import './styles/App.css';

function App() {
  return (
    <div className="app">
      <header className="app-header">
        <h1>bh Financial System</h1>
      </header>
      
      <main className="app-main">
        <section className="section">
          <h2>Customer Management</h2>
          <CustomerForm />
          <CustomerList />
        </section>

        <section className="section">
          <h2>Account Management</h2>
          <AccountForm />
        </section>

        <section className="section">
          <h2>Transaction Management</h2>
          <TransactionForm />
        </section>
      </main>
    </div>
  );
}

export default App;