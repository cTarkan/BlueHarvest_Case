import React, { useState } from 'react';

const CustomerForm = () => {
  const [name, setName] = useState('');
  const [surname, setSurname] = useState('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const response = await fetch('http://localhost:8080/api/customer', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ name, surname }),
      });
      if (response.ok) {
        const customer = await response.json();
        setName('');
        setSurname('');
        alert(`Customer created successfully! ID: ${customer.id}`);
      }
    } catch (error) {
      console.error('Error creating customer:', error);
    }
  };

  return (
    <form className="form" onSubmit={handleSubmit}>
      <div className="input-group">
        <label htmlFor="name">Name:</label>
        <input
          id="name"
          value={name}
          onChange={(e) => setName(e.target.value)}
          required
        />
      </div>
      <div className="input-group">
        <label htmlFor="surname">Surname:</label>
        <input
          id="surname"
          value={surname}
          onChange={(e) => setSurname(e.target.value)}
          required
        />
      </div>
      <button type="submit">Create Customer</button>
    </form>
  );
};

export default CustomerForm;