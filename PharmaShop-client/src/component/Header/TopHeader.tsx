
const TopHeader = () => {
  return (
    <div className="min-h-10 w-full flex justify-between items-center bg-primary px-8 text-sm">
      <p className="text-left text-white">
        Free Shipping for all Orders of <span className="font-bold">$99</span>
      </p>

      <div className="flex space-x-3 text-xs">
        <div className="social-icon">
          <i className="bi bi-instagram insta text-white"></i>
        </div>

        <div className="social-icon text-xs">
          <i className="bi bi-telegram telegram text-white "></i>
        </div>

        <div className="social-icon text-xs">
          <i className="bi bi-facebook facebook text-white"></i>
        </div>

        <div className="social-icon text-xs">
          <i className="bi bi-youtube youtube text-white"></i>
        </div>

        <div className="social-icon text-xs">
          <i className="bi bi-twitter-x twitter text-white"></i>
        </div>
      </div>
    </div>
  );
};

export default TopHeader;