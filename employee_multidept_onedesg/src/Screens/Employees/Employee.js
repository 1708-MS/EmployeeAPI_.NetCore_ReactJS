import axios from "axios";
import React, { useEffect, useState } from "react";
import AddEmployee from "./AddEmployee";
import EditEmployee from "./EditEmployee";
import Swal from "sweetalert2";

function Employee() {
  const [employees, setEmployees] = useState();
  const [employeeForm, setEmployeeForm] = useState([]);
  const [designations, setDesignations] = useState([]);
  const [departments, setDepartments] = useState([]);

  useEffect(() => {
    getAll();
    getAllDesignation();
    getAllDepartment();
  }, []);

  // Get All Saved Employees details
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

  //Get all details of the Saved Departments only
  function getAllDepartment() {
    axios
      .get("https://localhost:44347/api/Department")
      .then((d) => {
        setDepartments(
          d.data?.map((item) => {
            return { label: item?.departmentName, value: item?.departmentId };
          })
        );
      })
      .catch((e) => {
        alert("No data found");
      });
  }

  //Get all details of the Saved Designations only
  function getAllDesignation() {
    axios
      .get("https://localhost:44347/api/Designation")
      .then((d) => {
        setDesignations(
          d.data?.map((item) => {
            return { label: item?.designationName, value: item?.designationId };
          })
        );
        //  console.log(designations.designationId)
      })
      .catch((e) => {
        alert("no data found");
      });
  }

  //Change Handler to read the cjhange in values on input and dropdowns
  const changeHandler = (event) => {
    setEmployeeForm((prev) => {
      return {
        ...prev,
        [event.target.name]: event.target.value,
      };
    });
    console.log("Employee Details", employeeForm);
  };

  //Save the details of the NewEmployees including Departments(Single or Multiple) and Designation(Single only)
  // const saveClick = () => {
  //   debugger;
  //   // employeeForm.designationId=employeeForm.employeeDesignation;
  //   employeeForm.departmentIds = employeeForm.departmentId;
  //   axios
  //     .post("https://localhost:44347/api/Employee", employeeForm)
  //     .then((res) => {
  //       getAll();
  //       alert("data saved successfully");
  //     })
  //     .catch((error) => {
  //       alert("something went wrong with api");
  //     });
  // };

  //Edit the details of the saved Employees
  function editClick(data) {
    debugger;
    setEmployeeForm(data);
    console.log("edit", data);
    console.log(designations);
  }

  //Update the details of the edit Employees
  // const updateClick = (e) => {
  //   debugger;

  //   // employeeForm.designationId = e.target.value;
  //   console.log(employeeForm);
  //   axios
  //     .put("https://localhost:44347/api/Employee", employeeForm)
  //     .then((d) => {
  //       getAll();
  //       alert("data updated successfully");
  //     })
  //     .catch((e) => {
  //       alert("something went wrong with api");
  //     });
  // };

  //Delete the complete detials of the Saved Employees
  // function deleteClick(employeeId) {
  //   var ans = alert("Do u want to delete employee?");
  //   if (ans) return;

  //   axios
  //     .delete("https://localhost:44347/api/Employee/" + employeeId)
  //     .then((d) => {
  //       getAll();
  //     })
  //     .catch((e) => {
  //       alert("something went wrong. Plz try again.");
  //     });
  // }

  // function deleteClick(employeeId) {
  //   const confirmDelete = window.prompt("Are you sure you want to delete this employee? Enter 'yes' to confirm.");
  //   if (confirmDelete === "yes") {
  //     axios
  //       .delete("https://localhost:44347/api/Employee/" + employeeId)
  //       .then((d) => {
  //         getAll();
  //       })
  //       .catch((e) => {
  //         alert("something went wrong. Plz try again.");
  //       });
  //   }
  // }

  function deleteClick(employeeId) {
    Swal.fire({
      title: "Are you sure?",
      text: "Are you sure you want to delete this employee?",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Confirm",
      cancelButtonText: "Cancel",
    }).then((result) => {
      if (result.value) {
        // Make the DELETE request
        axios
          .delete("https://localhost:44347/api/Employee/" + employeeId)
          .then((d) => {
            getAll();
          })
          .catch((e) => {
            alert("something went wrong. Plz try again.");
          });
      }
    });
  }

  function getDropDown() {
    getAllDesignation();
    getAllDepartment();
  }

  //Method to render the details of the saved Employees
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
              onClick={() => editClick(item)}
              onChange={changeHandler}
              value={item.employeeId}
            >
              Edit
            </button>
            <button
              className="btn btn-danger m-1"
              onClick={() => deleteClick(item.employeeId)}
            >
              Delete
            </button>
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
        <table className="table table-striped table-bordered table-hover table-active">
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

      {/* Save/Add new Employees */}
      <AddEmployee
        getAll={() => getAll()}
        departments={departments}
        designations={designations}
      />

      {/* Edit Employees */}
      <EditEmployee
        getAll={() => getAll()}
        employeeForm={employeeForm}
        departments={departments}
        designations={designations}
      />
    </div>
  );
}

export default Employee;
