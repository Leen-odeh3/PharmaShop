import { NavLink, useNavigate } from "react-router-dom";
import "./Header.css";
import { useState } from "react";
import logo from "/Images/Logo.png";

const Navbar = () => {
  const [open, setOpen] = useState(false);
  const links = [
    "Home",
    "About",
    "Shop",
    "Product",
    "Blog",
    "Contact",
    "OnSales",
  ];
  const navigate = useNavigate();
  return (
    <header className="py-6 sm:py-2.5 px-10 flex justify-between items-center headerr">
      <button
        aria-expanded={open}
        className="bi bi-list text-black px-6 text-3xl md:hidden"
        onClick={() => setOpen(!open)}
      ></button>
      <img
        src={logo}
        alt="Logo"
        className="hidden sm:block"
        style={{ width: 180, height: 100 }}
      />

      <nav
        className={`text-[#E2E2E2] px-6 transition-transform duration-300 ${
          open ? "translate-x-0" : "-translate-x-full"
        } md:translate-x-0`}
      >
        <button
          className="bi bi-x-lg text-black cursor-pointer sm:hidden text-xl delete"
          onClick={() => setOpen(false)}
        ></button>
        {links.map((link) => (
          <NavLink
            onClick={() => setOpen(false)}
            className="p-2 link transition-colors duration-200 text-secondary font-bold"
            to={`/${link.toLowerCase().split(" ").join("-")}`}
            key={link}
          >
            {link}
          </NavLink>
        ))}
      </nav>

      <div className="flex justify-between items-center sm:px-8">
        <button
          className="font-bold text-secondary sm:px-3 px-6"
          onClick={() => navigate("login")}
        >
          SIGN IN
        </button>

        <div className="flex justify-between items-center">
          <i className="bi bi-heart text-xl	px-1"></i>
          <i className="bi bi-cart4 text-2xl pl-2"></i>
          <span className="pl-1">$00.0</span>
        </div>
      </div>
    </header>
  );
};

export default Navbar;