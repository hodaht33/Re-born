import { Link } from "react-router-dom";
import "./HrefLink.scss";

function HrefLink({ to, children }) {
    return <Link to={to} className="href-link"> {children}</Link>;
}

export default HrefLink;