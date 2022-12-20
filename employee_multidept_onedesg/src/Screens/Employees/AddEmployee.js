import axios from "axios";
import React from "react";
import { useForm } from "react-hook-form";
import InputField from "../../Components/FormFields/InputField";
import SelectField from "../../Components/FormFields/SelectField";



function AddEmployee({ getAll, departments, designations }) {
  const {
    control,
    handleSubmit,
    // setValue,
    // setError,
    // formState: { isSubmitting },
  } = useForm();

  const onSubmit = (formValues) => {
    console.log(formValues, "form values");
    axios
      .post("https://localhost:44347/api/Employee", formValues)
      .then((res) => {
        getAll();
        alert("data saved successfully");
      })
      .catch((error) => {
        alert("something went wrong with api");
      });
  };

  return (
    <>
      <form onSubmit={handleSubmit(onSubmit)}>
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
                  <label className="col-sm-4 text-left" for="txtName">
                    Employee Name
                  </label>
                  <div className="col-sm-8">
                    <InputField
                      control={control}
                      type="text"
                      name="employeeName"
                      rules={{
                        required: {
                          value: true,
                          message: "This field is required!",
                        },
                      }}
                    />
                    {/* <input
                      onChange={changeHandler}
                      type="text"
                      placeholder="Enter the Employee's Name"
                      name="employeeName"
                      id="txtName"
                      className="form-control"
                    /> */}
                  </div>
                </div>
                <div className="form-group row">
                  <label className="col-sm-4 text-left" for="txtAddress">
                    Employee Address
                  </label>
                  <div className="col-sm-8">
                    <InputField
                      control={control}
                      type="text"
                      name="employeeAddress"
                      rules={{
                        required: {
                          value: true,
                          message: "This field is required!",
                        },
                      }}
                    />
                    {/* <input
                      onChange={changeHandler}
                      type="text"
                      placeholder="Enter the Employee's Address"
                      name="employeeAddress"
                      id="txtAddress"
                      className="form-control"
                    /> */}
                  </div>
                </div>
                <div className="form-group row">
                  <label className="col-sm-4 text-left" for="txtSalary">
                    Employee Salary
                  </label>
                  <div className="col-sm-8">
                    <InputField
                      control={control}
                      type="text"
                      name="employeeSalary"
                      rules={{
                        required: {
                          value: true,
                          message: "This field is required!",
                        },
                      }}
                    />
                    {/* <input
                      onChange={changeHandler}
                      type="number"
                      placeholder="Enter the Employee's Salary"
                      name="employeeSalary"
                      id="txtSalary"
                      className="form-control"
                    /> */}
                  </div>
                </div>
                <div className="form-group row">
                  <label className="col-sm-4 text-left" for="txtDepartment">
                    Department Name
                  </label>
                  <div className="col-sm-8">
                    <SelectField
                      control={control}
                      name="departmentId"
                      multi={true}
                      options={departments}
                      rules={{
                        required: {
                          value: true,
                          message: "This field is required!",
                        },
                      }}
                    />
                    {/* <select
                      className="form-control basic-multi-select"
                      name="departmentId"
                      type="dropdown"
                      onChange={changeHandler}
                      //value={employeeForm.departmentId}
                      id="txtDepartment"
                    >
                      <option>Select one or multiple Departments</option>
                      {departments?.map((dept) => (
                        <option
                          value={dept.departmentId}
                          key={dept.departmentId}
                        >
                          {dept.departmentName}
                        </option>
                      ))}
                    </select> */}
                  </div>
                </div>
                <div className="form-group row">
                  <label className="col-sm-4 text-left" for="txtDesignation">
                    Designation Name
                  </label>
                  <div className="col-sm-8">
                    <SelectField
                      control={control}
                      name="designationId"
                      options={designations}
                      rules={{
                        required: {
                          value: true,
                          message: "This field is required!",
                        },
                      }}
                    />
                    {/* <select
                      className="form-control"
                      name="designationId"
                      type="dropdown"
                      onChange={changeHandler}
                      id="txtDesignation"
                    >
                      <option>Select a Designation</option>
                      {designations?.map((desg) => (
                        <option
                          value={desg.designationId}
                          key={desg.designationId}
                        >
                          {desg.designationName}
                        </option>
                      ))}
                    </select> */}
                  </div>
                </div>
                {/* Footer */}
                <div className="modal-footer">
                  <button
                    type="submit"
                    className="btn btn-success form-control"
                  >
                    Save
                  </button>
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
    </>
  );
}

export default AddEmployee;
