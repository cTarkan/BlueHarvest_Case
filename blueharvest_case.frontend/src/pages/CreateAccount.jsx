import { useState } from "react";
import { createAccount } from "../api/api";

function CreateAccount() {
    const [customerId, setCustomerId] = useState("");
    const [initialCredit, setInitialCredit] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();
        await createAccount(Number(customerId), Number(initialCredit));
        alert("Account created successfully!");
    };

    return (
        <div>
            <h1>Create Account</h1>
            <form onSubmit={handleSubmit}>
                <input type="text" placeholder="Customer ID" value={customerId} onChange={(e) => setCustomerId(e.target.value)} />
                <input type="text" placeholder="Initial Credit" value={initialCredit} onChange={(e) => setInitialCredit(e.target.value)} />
                <button className="button">Create</button>
            </form>
        </div>
    );
}

export default CreateAccount;
