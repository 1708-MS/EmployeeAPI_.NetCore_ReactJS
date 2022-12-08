import React from 'react'
import { Link } from 'react-router-dom'

function Header() {
  return (
    <div>
        <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <div class="collapse navbar-collapse" id="navbarNav">
          <ul class="navbar-nav">
            <li class="nav-item active">
              <Link className="nav-link" to="/home">Home</Link>
            </li>
            <li class="nav-item">
              <Link className="nav-link" to="/employee">Employee</Link>
            </li>
            <li className='nav-item'>
              <Link className='nav-link' to="/designation">Designation</Link>
            </li>
            <li>
              <Link className='nav-link' to="/department">Department</Link>
            </li>
          </ul>
        </div>
      </nav>
    </div>
  )
}

export default Header