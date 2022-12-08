import React from 'react'

function Employee() {
  return (
    <div>
      <div className='row'>
        <div className='mx-auto'>
          <h2 className='text-primary'>Employee List</h2>
        </div>
      </div>
      <div className='text-left p-2 m-2'>
          <button className='btn btn-info' data-toggle="modal" data-target="#newModal"><i className='fa fa-plus'>&nbsp;</i>
            New Employee</button>
        </div>
      <div className='p-2 m-2'>
        <table className='table table-stripped table-bordered table-hover table-active'>
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
        </table>
      </div>
    </div>
  )
}

export default Employee