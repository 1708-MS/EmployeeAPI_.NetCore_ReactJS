import logo from "./logo.svg";
import "./App.css";
import { BrowserRouter,Routes,Route } from 'react-router-dom';
import Designation from "./Screens/Designation";
import Department from "./Screens/Department";
import Employee from "./Screens/Employee";
import Header from "./Screens/Header";
import Home from "./Screens/Home";

function App() {
  return (
    <div className="App">
      <BrowserRouter>
      <Header/>
      <Routes>
        <Route path="/home" element={<Home/>}/>
        <Route path="/designation" element={<Designation/>} />
        <Route path="/department" element={<Department/>} />
        <Route path="/employee" element={<Employee/>} />
      </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
