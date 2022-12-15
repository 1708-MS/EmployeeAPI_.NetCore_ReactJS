import axios from "axios";
import React, { useEffect, useState } from "react";


function Employee() {
  const [employees, setEmployees] = useState();
  const [employeeForm, setEmployeeForm] = useState({});
  const [designations, setDesignations] = useState([]);
  const [departments, setDepartments] = useState([]);

  useEffect(() => {
    getAll();
  }, []);

  function getAll() {
    axios
      .get("https://localhost:44347/api/Employee")
      .then((d) => {
        setEmployees(d.data);
        console.log("data", d.data);
        console.log(employees);
      })
      .catch((e) => {
        alert("no data found");
      });
  }

  function getAllDepartment() {
    axios
      .get("https://localhost:44347/api/Department")
      .then((d) => {
        setDepartments(d.data);
      })
      .catch((e) => {
        alert("No data found");
      });
  }

  function getAllDesignation() {
    axios
      .get("https://localhost:44347/api/Designation")
      .then((d) => {
        setDesignations(d.data);
      //  console.log(designations.designationId)
      })
      .catch((e) => {
        alert("no data found");
      });
  }

  const changeHandler = (event) => {
    setEmployeeForm({
      ...employeeForm,
      [event.target.name]: event.target.value,
    });
    console.log("Employee Details",employeeForm);
  };

  function deleteClick(employeeId){
    axios.delete("https://localhost:44347/api/Employee/"+employeeId).then((d)=>{
        getAll();
    }).catch((e)=>{
        alert('something went wrong. Plz try again.')
    })
}

  function getDropDown(){
    getAllDesignation();
    getAllDepartment();
  }

  function renderEmployees(data) {
    let employeesRows = [];
    employees?.map((item) =>
      employeesRows.push(
        <tr>
          <td>{item.employeeName}</td>
          <td>{item.employeeAddress}</td>
          <td>{item.employeeSalary}</td>
          <td>{item.designationName}</td>
          <td>{item.designationCode}</td>
          <td>{item.departmentName.join(" & ")}</td>
          <td>{item.departmentCode.join(", ")}</td>
          <td>
            <button
              className="btn btn-info m-1"
              data-toggle="modal"
              data-target="#editModal"
              //onClick={()=>editClick(item)}
            >
              Edit
            </button>
            <button className="btn btn-danger m-1" onClick={()=>deleteClick(item.employeeId)}>Delete</button>
          </td>
        </tr>
      )
    );
    return employeesRows;
  }

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
          onClick={getDropDown}
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
        <div className="modal fade" id="newModal" role="dialog">
          <div className="modal-dialog">
            <div className="modal-content">
              {/* Header */}
              <div className="modal-header">
                <div className="modal-title text-primary">New Employee</div>
                <button className="close btn btn-danger" data-dismiss="modal">
                  <span>&times;</span>
                </button>
              </div>
              {/* Body */}
              <div className="modal-body">
                <div className="form-group row">
                  <label className="col-sm-4 text-left" for="txtName" >Employee Name</label>
                  <div className="col-sm-8">
                    <input
                      onChange={changeHandler}
                      type="text"
                      placeholder="Enter the Employee's Name"
                      name="name"
                      id="txtName"
                      className="form-control"
                    />
                  </div>
                </div>
                <div className="form-group row">
                  <label className="col-sm-4 text-left" for="txtAddress" >Employee Address</label>
                  <div className="col-sm-8">
                    <input
                      onChange={changeHandler}
                      type="text"
                      placeholder="Enter the Employee's Address"
                      name="address"
                      id="txtAddress"
                      className="form-control"
                      
                    />
                  </div>
                </div>
                <div className="form-group row">
                  <label className="col-sm-4 text-left" for="txtSalary" >Employee Salary</label>
                  <div className="col-sm-8">
                    <input
                      onChange={changeHandler}
                      type="text"
                      placeholder="Enter the Employee's Salary"
                      name="salary"
                      id="txtSalary"
                      className="form-control"
                    />
                  </div>
                </div>
                <div className="form-group row">
                  <label className="col-sm-4 text-left">Department Name</label>
                  <div className="col-sm-8">
                    <select
                      className="form-control basic-multi-select"
                      name="department"
                      options={getAllDepartment}
                      onChange={changeHandler}
                    >
                      <option>
                        Select one or multiple Departments
                      </option>
                      {departments?.map((dept) => (
                        <option value={dept.departmentId}>
                          {dept.departmentName}
                        </option>
                      ))}
                    </select>
                  </div>
                </div>
                <div className="form-group row">
                  <label className="col-sm-4 text-left">Designation Name</label>
                  <div className="col-sm-8">
                    <select
                     className="form-control"
                     name="designation"
                     type="text"
                     onChange={changeHandler}
                     >
                      <option>Select a Designation</option>
                      {designations?.map((desg) => (
                        <option
                          value={desg.designationId}
                        >
                          {desg.designationName}
                        </option>
                      ))}
                    </select>
                  </div>
                </div>
                {/* Footer */}
                <div className="modal-footer">
                  <button className="btn btn-success form-control">Save</button>
                  <button
                    className="btn btn-danger form-control"
                    data-dismiss="modal"
                  >
                    Cancel
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </form>

    {/* Edit Employees */}
    <form>
        <div className="modal" id="editModal" role="dialog">
          <div className="modal-dialog">
            <div className="modal-content">
              {/* Header */}
              <div className="modal-header">
                <div className="modal-title text-primary">Edit Employee</div>
                <button className="close btn btn-danger">
                  <span>&times;</span>
                </button>
              </div>
              {/* Body */}
              <div className="modal-body">
                <div className="form-group row">
                  <label className="col-sm-4 text-left" for="txtName">Employee Name</label>
                  <div className="col-sm-8">
                    <input
                      onChange={changeHandler}
                      type="text"
                      placeholder="Enter the Employee's Name"
                      name="name"
                      id="txtName"
                      value={employeeForm.employeeName}
                      className="form-control"
                    />
                  </div>
                </div>
                <div className="form-group row">
                  <label className="col-sm-4 text-left" for="txtAddress">Employee Address</label>
                  <div className="col-sm-8">
                    <input
                      onChange={changeHandler}
                      type="text"
                      placeholder="Enter the Employee's Address"
                      name="address"
                      id="txtAddress"
                      value={employeeForm.employeeAddress}
                      className="form-control"
                    />
                  </div>
                </div>
                <div className="form-group row">
                  <label className="col-sm-4 text-left" for="txtSalary">Employee Salary</label>
                  <div className="col-sm-8">
                    <input
                      onChange={changeHandler}
                      type="text"
                      placeholder="Enter the Employee's Salary"
                      name="salary"
                      id="txtSalary"
                      value={employeeForm.employeeSalary}
                      className="form-control"
                    />
                  </div>
                </div>
                <div className="form-group row">
                  <label className="col-sm-4 text-left">Department Name</label>
                  <div className="col-sm-8">
                    <select
                      onChange={changeHandler}
                      className="form-control basic-multi-select"
                      type="text"
                      name="department"
                    >
                      <option value="0">
                        Select one or multiple Departments
                      </option>
                      {departments?.map((dept) => (
                        <option value={dept.departmentId}>
                          {dept.departmentName}
                        </option>
                      ))}
                    </select>
                  </div>
                </div>
                <div className="form-group row">
                  <label className="col-sm-4 text-left">Designation Name</label>
                  <div className="col-sm-8">
                    <select
                     className="form-control"
                     name="designation"
                     type="text"
                     onChange={changeHandler}
                     >

                      {designations?.map((desg) => (
                        <option
                          value={desg.designationId}
                        >
                          {desg.designationName}
                        </option>
                      ))}
                    </select>
                  </div>
                </div>
                {/* Footer */}
                <div className="modal-footer">
                  <button className="btn btn-success form-control">Update</button>
                  <button
                    className="btn btn-danger form-control"
                    data-dismiss="modal"
                  >
                    Cancel
                  </button>
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
