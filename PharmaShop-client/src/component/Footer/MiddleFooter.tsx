import { NavLink } from "react-router-dom";
import logo from "/Images/Logo.png";

const MiddleFooter = () => {
  const links = [
    "Home",
    "About",
    "Shop",
    "Product",
    "Blog",
    "Contact",
    "OnSales",
  ];
  const categories = [
    "Devices",
    "FamilyCare",
    "Fitness",
    "LifeStyle",
    "PersonalCare",
    "Health",
    "Vitamens",
  ];
  const paymentMethods = [
    "CreditCard",
    "PayPal",
    "BankTransfer",
    "Visa",
    "In Shop",
    "OnDelivery",
    "MasterCard",
  ];

  return (
    <div
      className=" grid grid-cols-1 sm:grid-cols-3 gap-2 rounded-100 p-2 m-2 my-3"
      style={{
        borderTop: ".2px solid #f0eeeb",
        borderBottom: ".2px solid #f0eeeb",
      }}
    >
      <div
        className="p-4 flex flex-col items-center"
        style={{
          borderRight: ".2px solid #f0eeeb",
          height: "100%",
        }}
      >
        <img
          src={logo}
          alt="PharmacyLogo"
          height="80px"
          width="160px"
          className="mb-2"
        />
        <p className="text-center text-gray" style={{ textAlign: "start" }}>
          Pharmacy 2U Shop is proud of being the best Pharmacy Online shop in
          Palestine with high-quality medicines, supplements, healthcare
          products, …
        </p>
      </div>

      <div
        className="p-2 flex justify-center align-middle"
        style={{
          borderRight: ".2px solid #f0eeeb",
        }}
      >
        <div className="flex md:flex-row items-center justify-center space-x-4">
          <div className="flex flex-col items-center">
            <h2 className="font-bold py-1">Pages</h2>
            <nav className="text-[#E2E2E2] px-6">
              {links.map((link) => (
                <NavLink
                  className="p-2 link duration-200 text-gray block"
                  to={`/${link.toLowerCase().split(" ").join("-")}`}
                  key={link}
                >
                  {link}
                </NavLink>
              ))}
            </nav>
          </div>

          <div className="flex flex-col items-center">
            <h2 className="font-bold py-1">Categories</h2>
            <nav className="text-[#E2E2E2] px-6">
              {categories.map((category) => (
                <NavLink
                  className="p-2 link duration-200 text-gray block"
                  to={`/categories/${category
                    .toLowerCase()
                    .split(" ")
                    .join("-")}`}
                  key={category}
                >
                  {category}
                </NavLink>
              ))}
            </nav>
          </div>

          <div className="flex flex-col items-center">
            <h2 className="font-bold py-1">Payments</h2>
            <nav className="text-[#E2E2E2] px-6">
              {paymentMethods.map((method) => (
                <NavLink
                  className="p-2 link duration-200 text-gray block"
                  to={`/payment-methods/${method
                    .toLowerCase()
                    .split(" ")
                    .join("-")}`}
                  key={method}
                >
                  {method}
                </NavLink>
              ))}
            </nav>
          </div>
        </div>
      </div>

      <div className="p-4 ">
        <div className="hidden sm:flex flex-col items-center">
          <p className="font-bold text-center py-3 text-xl">Socials</p>
          <a
            href="https://www.youtube.com"
            className="flex items-center text-red-600 hover:text-red-800 mb-2"
            target="_blank"
            rel="noopener noreferrer"
          >
            <i className="bi bi-youtube text-2xl mr-2"></i>
            <span className="link duration-200 text-gray block">
              YouTube
            </span>
          </a>
          <a
            href="https://telegram.org"
            className="flex items-center text-blue-500 hover:text-blue-700 mb-2"
            target="_blank"
            rel="noopener noreferrer"
          >
            <i className="bi bi-telegram text-2xl mr-2"></i>
            <span className="link duration-200 text-gray block">
              Telegram
            </span>
          </a>
          <a
            href="https://www.instagram.com"
            className="flex items-center text-pink-500 hover:text-pink-700 mb-2"
            target="_blank"
            rel="noopener noreferrer"
          >
            <i className="bi bi-instagram text-2xl mr-2"></i>
            <span className="link duration-200 text-gray block">
              Instagram
            </span>
          </a>
          <a
            href="https://www.facebook.com"
            className="flex items-center text-blue-600 hover:text-blue-800 mb-2"
            target="_blank"
            rel="noopener noreferrer"
          >
            <i className="bi bi-facebook text-2xl mr-2"></i>
            <span className="link duration-200 text-gray block">
              Facebook
            </span>
          </a>
          <a
            href="https://twitter.com"
            className="flex items-center text-blue-400 hover:text-blue-600 mb-2"
            target="_blank"
            rel="noopener noreferrer"
          >
            <i className="bi bi-twitter text-2xl mr-2"></i>
            <span className="p-1 link duration-200 text-gray block">
              Twitter
            </span>
          </a>
        </div>
      </div>
    </div>
  );
};

export default MiddleFooter;