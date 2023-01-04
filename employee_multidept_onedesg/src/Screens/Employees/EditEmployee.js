//import axios from "axios";
import axios from "axios";
import React, { useEffect } from "react";
import { useForm } from "react-hook-form";
import InputField from "../../Components/FormFields/InputField";
import SelectField from "../../Components/FormFields/SelectField";

function EditEmployee({ getAll, employeeForm, departments, designations }) {
  const {
    control,
    handleSubmit,
    setValue,
    // setError,
    // formState: { isSubmitting },
  } = useForm();

  const onSubmit = (formValues) => {
    debugger;
    console.log(formValues, "form values");
    axios
      .put(`https://localhost:44347/api/Employee`, {
        ...formValues,
        employeeId: employeeForm?.employeeId,
      })
      .then((res) => {
        getAll();
        alert("data saved successfully");
      })
      .catch((error) => {
        alert("something went wrong with api");
      });
  };

  useEffect(() => {
    setValue("employeeName", employeeForm?.employeeName);
    setValue("employeeAddress", employeeForm?.employeeAddress);
    setValue("employeeSalary", employeeForm?.employeeSalary);
    setValue("departmentIds", employeeForm?.departmentIds);
    setValue("designationId", employeeForm?.designationId);
  }, [setValue, employeeForm]);

  return (
    <>
      <form onSubmit={handleSubmit(onSubmit)}>
        <div className="modal fade" id="editModal" role="dialog">
          <div className="modal-dialog">
            <div className="modal-content">
              {/* Header */}
              <div className="modal-header">
                <div className="modal-title text-primary">Edit Employee</div>
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
                  </div>
                </div>
                <div className="form-group row">
                  <label className="col-sm-4 text-left" for="txtSalary">
                    Employee Salary
                  </label>
                  <div className="col-sm-8">
                    <InputField
                      control={control}
                      type="number"
                      name="employeeSalary"
                      rules={{
                        required: {
                          value: true,
                          message: "This field is required!",
                        },
                      }}
                    />
                  </div>
                </div>
                <div className="form-group row">
                  <label className="col-sm-4 text-left" for="txtDepartment">
                    Department Name
                  </label>
                  <div className="col-sm-8">
                    <SelectField
                      control={control}
                      name="departmentIds"
                      multi={true}
                      options={departments}
                      rules={{
                        required: {
                          value: true,
                          message: "This field is required!",
                        },
                      }}
                    />
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

export default EditEmployee;
