import { BrowserRouter as Router, Route, Routes, Link } from "react-router-dom";
import Home from "./pages/Home";
import CreateAccount from "./pages/CreateAccount";
import AddTransaction from "./pages/AddTransaction";
import "./index.css";

function App() {
    return (
        <Router>
            <div className="container">
                <h1>BlueHarvest Bank</h1>
                <nav>
                    <Link className="button" to="/">Accounts</Link>
                    <Link className="button" to="/create-account">Create Account</Link>
                    <Link className="button" to="/add-transaction">Add Transaction</Link>
                </nav>
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/create-account" element={<CreateAccount />} />
                    <Route path="/add-transaction" element={<AddTransaction />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;
