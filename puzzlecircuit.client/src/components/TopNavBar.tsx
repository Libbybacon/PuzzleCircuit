import { Link, useNavigate } from '@tanstack/react-router';
import { useAuth } from '@/context/AuthContext';
import Button from 'react-bootstrap/Button';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';

export default function TopNavBar() {
    const { user, logout } = useAuth();
    const navigate = useNavigate();

    const handleLogout = async () => {
        await logout();
        void navigate({ to: '/login' });
    };

    return (
        <Navbar expand="lg" className="p-1 top-nav">
            <div className="d-flex w-100 text-cream px-2 m-0">
                <Navbar.Brand className="p-1" href="#home">
                    <img
                        src="/logo.png"
                        width="27"
                        height="48"
                        className="d-inline-block align-middle"
                        alt="Puzzle Circuit logo"
                    />
                </Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="me-auto">
                        <Nav.Link as={Link} to="/dashboard" className="navbar__link">Home</Nav.Link>
                        <Nav.Link as={Link} to="/events" className="navbar__link">Events</Nav.Link>
                    </Nav>
                    <span className="ms-auto me-2"> {user?.displayName} </span>
                    <Button onClick={handleLogout} className="navbar__signout"> Sign Out</Button>
                </Navbar.Collapse>
            </div>
        </Navbar>
    );
}
