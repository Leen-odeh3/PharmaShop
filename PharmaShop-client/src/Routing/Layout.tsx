
import Header from "../component/Header/Header";
import Footer from "../component/Footer/Footer";
import { LayoutProps } from "../Interfaces/LayoutProps";

const Layout: React.FC<LayoutProps> = ({ children }) => {
  return (
    <>
      <Header />
      <main>{children}</main>
      <Footer />
    </>
  );
};

export default Layout;
