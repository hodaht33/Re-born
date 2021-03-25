import { Card } from "react-bootstrap";
import './FAQCard.scss';

export default function FAQCard({ title, subtitle, content }) {
    return (
        <div className="faq-card">
            <Card.Body>
                <Card.Title>{title}</Card.Title>
                <Card.Subtitle className="mt-3 mb-3 text-muted">{subtitle}</Card.Subtitle>
                <Card.Text className="faq-content">
                    {content}
                </Card.Text>
            </Card.Body>
        </div>
    );
}