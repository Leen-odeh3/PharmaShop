import { lazy } from "react";

// Lazy loading components
export const About = lazy(() => import("../pages/About/About"));
export const Blog = lazy(() => import("../pages/Blog/Blog"));
export const Seals = lazy(() => import("../pages/OnSeals/Seals"));
export const Shop = lazy(() => import("../pages/Shop/Shop"));
export const Contact = lazy(() => import("../pages/Contact/Contact"));
export const Product = lazy(() => import("../pages/Dashboard/Product/Product"));
export const Home = lazy(() => import("../pages/Home/Home"));
export const Footer = lazy(() => import("../component/Footer/Footer"));
export const Header = lazy(() => import("../component/Header/Header"));
export const NotFound = lazy(() => import("../pages/NotFound/NotFound"));
export const User = lazy(() => import("../pages/Dashboard/Customer/User"));
export const Login = lazy(() => import("../forms/Login"));
export const Profile = lazy(() => import("../pages/Profile/Profile"));
export const ForgotPass = lazy(() => import("../forms/ForgotPassword"));
export const Register = lazy(() => import("../forms/Register"));
export const Order = lazy(() => import("../pages/Dashboard/Order/Order"));
export const Category = lazy(() => import("../pages/Dashboard/Category/Category"));
export const Transaction = lazy(() => import("../pages/Dashboard/Transaction/Transaction"));
export const Brand = lazy(() => import("../pages/Dashboard/Brand/Brand"));
export const MainDash =lazy(()=> import ("../pages/Dashboard/Admin/Dash"));
export const BarChart=lazy(()=> import ("../pages/Chart/BarChart"));
export const LineChart =lazy(()=> import ("../pages/Chart/LineChart"));
export const PieChart =lazy(()=> import ("../pages/Chart/PieChart"));