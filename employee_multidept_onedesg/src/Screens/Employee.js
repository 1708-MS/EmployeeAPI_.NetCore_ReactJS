import axios from "axios";
import React, { useEffect, useState } from "react";

function Employee() {
  const [employees, setEmployees] = useState(null);
  // const[employeeForm, setEmployeeForm] = useState({});

  useEffect(() => {
    getAll();
  },[]);

  function getAll() {
    axios.get("https://localhost:44347/api/Employee")
      .then((d) => {
        setEmployees(d.data);
        console.log("data",d.data)
        console.log(employees);
      })
      .catch((e) => {
        alert("no data found");
      });
  }

  function renderEmployees() {
    let employeesRows = [];
    employees?.map((item) =>(
      employeesRows.push(
        <tr>
          <td>{item.employeeName}</td>
          <td>{item.employeeAddress}</td>
          <td>{item.employeeSalary}</td>
          <td>{item.designationName}</td>
          <td>{item.designationCode}</td>
          <td>{item.departmentName}</td>
          <td>{item.departmentCode}</td>
          <td>
            <button
              className="btn btn-info"
              data-toggle="modal"
              data-target="#editModal"
            >
              Edit
            </button>
            <button className="btn btn-danger">Delete</button>
          </td>
        </tr>
      )
    ));
    return employeesRows;
  };

  return (
    <div>
      <div className="row">
        <div className="mx-auto">
          <h2 className="text-primary">Employee List</h2>
        </div>
      </div>
      <div className="text-left p-2 m-2">
        <button
          className="btn btn-info"
          data-toggle="modal"
          data-target="#newModal"
        >
          <i className="fa fa-plus">&nbsp;</i>
          New Employee
        </button>
      </div>
      <div className="p-2 m-2">
        <table className="table table-stripped table-bordered table-hover table-active">
          <thead>
            <tr>
              <th>Name</th>
              <th>Address</th>
              <th>Salary</th>
              <th>Designation Name</th>
              <th>Designation Code</th>
              <th>Department Name</th>
              <th>Department Codes</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>{renderEmployees()}</tbody>
        </table>
      </div>

      {/* Save Employees */}

      <form>
        <div className="modal" id="newModal" role="dialog">
          <div className="modal-dialog">
            <div className="modal-content">
              {/* Header */}
              <div className="modal-header">
                <div className="modal-title text-primary">New Employee</div>
                <button className="close btn btn-danger">
                  <span>&times;</span>
                </button>
              </div>
              {/* Body */}
              <div className="modal-body">
                <div className="form-group row">
                  <label className="col-sm-4">Employee Name</label>
                  <div className="col-sm-8">
                    <input
                      type="text"
                      placeholder="Enter the Employee's Name"
                      name="name"
                      id="txtname"
                      className="form-control"
                    />
                  </div>
                </div>
                <div className="form-group row">
                  <label className="col-sm-4">Employee Address</label>
                  <div className="col-sm-8">
                    <input
                      type="text"
                      placeholder="Enter the Employee's Address"
                      name="address"
                      id="txtAddress"
                      className="form-control"
                    />
                  </div>
                </div>
                <div className="form-group row">
                  <label className="col-sm-4">Salary</label>
                  <div className="col-sm-8">
                    <input
                      type="text"
                      placeholder="Enter the Employee's Salary"
                      name="salary"
                      id="txtSalary"
                      className="form-control"
                    />
                  </div>
                </div>
                <div className="form-group row">
                  <label className="col-sm-4">Department Name</label>
                  <div className="col-sm-8">
                    <select>
                      <option value="0"></option>
                    </select>
                  </div>
                </div>
                <div className="form-group row">
                  <label className="col-sm-4">Designation Name</label>
                  <div className="col-sm-8">
                    <select>
                      <option value="0">Select Designation</option>
                    </select>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </form>
    </div>
  );
}

export default Employee;
