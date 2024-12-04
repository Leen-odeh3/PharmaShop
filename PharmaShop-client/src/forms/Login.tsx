import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";

const Login = () => {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [rememberMe, setRememberMe] = useState<boolean>(false);
  const [errorMessage, setErrorMessage] = useState<string>("");

  const nav = useNavigate();

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) {
      const loggedIn = localStorage.getItem("username");
      if (loggedIn) {
        nav("/dashboard/admin");
      }
    }
  }, [nav]);
  
  
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!email || !password) {
      setErrorMessage("Please enter both email and password.");
      return;
    }

    setErrorMessage("");

    const payload = {
      email,
      password,
    };

    try {
      const response = await axios.post(
        "http://localhost:5068/api/Auth/login",
        payload
      );

      if (response.data.succeeded) {
        const token = response.data.data.token;
        const username = response.data.data.username;
        const roles = response.data.data.roles;

        localStorage.setItem("token", token);
        localStorage.setItem("username", username);

        nav("/dashboard/admin");
      } else {
        setErrorMessage(response.data.message || "Invalid login credentials.");
      }
    } catch (error: any) {
      console.error("Login error:", error);
      setErrorMessage(error.response?.data?.message || "An error occurred. Please try again later.");
    }
  };

  return (
    <div className="flex justify-center items-center pb-4 pt-16">
      <div className="w-96 p-6 shadow-lg rounded-md">
        <h1 className="text-3xl block text-center font-semibold">
          <i className="fa-solid fa-user"></i> Login
        </h1>

        <form onSubmit={handleSubmit} className="mt-3">
          <div className="mt-3">
            <label htmlFor="email" className="block text-base mb-1">
              Email
            </label>
            <input
              type="email"
              id="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              className="w-full text-base px-2 py-1 focus:outline-none focus:ring-0 border-b"
              placeholder="Enter email..."
              style={{ borderBottomColor: "#f0eeeb" }}
            />
          </div>

          <div className="mt-3">
            <label htmlFor="password" className="block text-base mb-1">
              Password
            </label>
            <input
              type="password"
              id="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              className="w-full text-base px-2 py-1 focus:outline-none focus:ring-0 focus:border-gray-600 border-b"
              placeholder="Enter Password..."
              style={{ borderBottomColor: "#f0eeeb" }}
            />
          </div>

          <div className="mt-3 flex justify-between items-center">
            <div className="flex items-center">
              <input
                type="checkbox"
                id="rememberMe"
                checked={rememberMe}
                onChange={(e) => setRememberMe(e.target.checked)}
                className="mr-2"
              />
              <label htmlFor="rememberMe" className="text-base">
                Remember Me
              </label>
            </div>
            <div>
              <button
                className="block py-1"
                type="button"
                onClick={() => nav("/forgot-pass")}
              >
                Forgot password
              </button>
            </div>
          </div>

          {errorMessage && (
            <div className="mt-3 text-red-600 text-center">
              {errorMessage}
            </div>
          )}

          <div className="mt-5">
            <button
              type="submit"
              className="border bg-secondary text-white py-2 w-full rounded-md hover:bg-transparent hover:text-primary font-semibold"
            >
              <i className="fa-solid fa-right-to-bracket"></i>&nbsp;&nbsp;Login
            </button>
          </div>

          <div className="mt-5">
            <button
              type="button"
              onClick={() => nav("/forgot-pass")}
              className="block py-1"
            >
              Lost your password?
            </button>
            <button
              type="button"
              onClick={() => nav("/register")}
            >
              Don't have an account?{" "}
              <span className="font-bold text-primary">SignUp</span>
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default Login;
