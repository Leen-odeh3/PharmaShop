import { useNavigate } from "react-router-dom";
import { useFormik } from "formik";
import axios from "axios";
import { RegistrationValidation } from "../Utils/RegistrationValidation"; 

const Register = () => {
  const nav = useNavigate();

  const formik = useFormik({
    initialValues: {
      firstName: "",
      lastName: "",
      email: "",
      password: "",
      address: "",
    },
    validationSchema: RegistrationValidation, 
    onSubmit: async (values) => {
      console.log("Form Data: ", values);
    
      try {
        const response = await axios.post("http://localhost:5068/api/Auth/register-customer", values);
    
        if (response.data.succeeded) {
          nav("/login");
        } else {
          alert("Registration failed: " + response.data.message);
        }
      } catch (error) {
        console.error("Registration error:", error);
        alert("An error occurred while registering. Please try again.");
      }
    },
    
  });

  return (
    <div className="flex justify-center items-center pt-4">
      <div className="w-5/12 px-6 pb-7 pt-6 shadow-lg rounded-md">
        <h1 className="text-3xl block text-center font-semibold">
          Register
        </h1>

        <form onSubmit={formik.handleSubmit} className="mt-3 w-full">
          <div>
            <div className="mt-3 pr-6">
              <label htmlFor="firstName" className="block text-base mb-1">
                First Name
              </label>
              <input
                type="text"
                id="firstName"
                {...formik.getFieldProps("firstName")}
                className={`w-full text-base px-2 py-1 focus:outline-none focus:ring-0 ${
                  formik.touched.firstName && formik.errors.firstName
                    ? "border-red-500"
                    : ""
                }`}
                placeholder="Enter First Name..."
              />
              {formik.touched.firstName && formik.errors.firstName && (
                <div className="text-red-500 text-sm">{formik.errors.firstName}</div>
              )}
            </div>

            <div className="mt-3">
              <label htmlFor="lastName" className="block text-base mb-1">
                Last Name
              </label>
              <input
                type="text"
                id="lastName"
                {...formik.getFieldProps("lastName")}
                className={`w-full text-base px-2 py-1 focus:outline-none focus:ring-0 border-b ${
                  formik.touched.lastName && formik.errors.lastName
                    ? "border-red-500"
                    : ""
                }`}
                placeholder="Enter Last Name..."
              />
              {formik.touched.lastName && formik.errors.lastName && (
                <div className="text-red-500 text-sm">{formik.errors.lastName}</div>
              )}
            </div>
          </div>

          <div>
            <div className="mt-3 pr-6">
              <label htmlFor="email" className="block text-base mb-1">
                Email
              </label>
              <input
                type="email"
                id="email"
                {...formik.getFieldProps("email")}
                className={`w-full text-base px-2 py-1 focus:outline-none focus:ring-0 border-b ${
                  formik.touched.email && formik.errors.email ? "border-red-500" : ""
                }`}
                placeholder="Enter Email..."
              />
              {formik.touched.email && formik.errors.email && (
                <div className="text-red-500 text-sm">{formik.errors.email}</div>
              )}
            </div>

            <div className="mt-3">
              <label htmlFor="password" className="block text-base mb-1">
                Password
              </label>
              <input
                type="password"
                id="password"
                {...formik.getFieldProps("password")}
                className={`w-full text-base px-2 py-1 focus:outline-none focus:ring-0 border-b ${
                  formik.touched.password && formik.errors.password ? "border-red-500" : ""
                }`}
                placeholder="Enter Password..."
              />
              {formik.touched.password && formik.errors.password && (
                <div className="text-red-500 text-sm">{formik.errors.password}</div>
              )}
            </div>
          </div>

          <div className="mt-3">
            <label htmlFor="address" className="block text-base mb-1">
              Address
            </label>
            <input
              type="text"
              id="address"
              {...formik.getFieldProps("address")}
              className={`w-full text-base px-2 py-1 focus:outline-none focus:ring-0 border-b ${
                formik.touched.address && formik.errors.address ? "border-red-500" : ""
              }`}
              placeholder="Enter Address..."
            />
            {formik.touched.address && formik.errors.address && (
              <div className="text-red-500 text-sm">{formik.errors.address}</div>
            )}
          </div>

          <div className="mt-3 flex justify-between items-center">
            <p className="text-gray text-xs mt-3 mb-3 text-center">
              Your personal data will be used to support your experience
              throughout this website, to manage access to your account, and for
              other purposes described in our privacy policy.
            </p>
          </div>

          <div className="mt-5 text-center">
            <button
              type="submit"
              className="border bg-secondary text-white py-2 w-2/4 text-center rounded-md hover:bg-transparent hover:text-primary font-semibold"
            >
              <i className="fa-solid fa-right-to-bracket"></i>
              &nbsp;&nbsp;Register
            </button>
          </div>

          <div className="mt-5 text-center">
            <button onClick={() => nav("/login")}>
              Already have an account?{" "}
              <span className="font-bold text-primary">Login</span>
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default Register;
