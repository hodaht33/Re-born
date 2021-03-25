import { CardDeck } from "react-bootstrap";
import FAQCard from "./FAQCard/FAQCard";
import './FAQ.scss';

const faqs = [
    {
        title: '게임 초보자인데 처음 해볼 만한 게임을 추천해줄 수 있나요?',
        subtitle: 'This is FAQ subtitle',
        content: 'FAQ 답변 공간입니다. 분명하고 정확하게 FAQ 내용을 작성하세요. 작성한 내용을 다시 한번 검토해보세요. 홈페이지를 처음 방문한 사람이 FAQ에 실린 내용을 읽고 충분히 이해할 수 있을지 생각해보면 도움이 됩니다. 필요하다면 내용을 수정하거나 보충하세요. 사진이나 동영상과 같은 시각 자료를 활용하면 한층 뚜렷한 인상을 남길 수 있습니다.'
    },
    {
        title: '신규 콘텐츠는 얼마나 자주 업데이트되나요?',
        subtitle: 'This is FAQ subtitle',
        content: 'FAQ 답변 공간입니다. 분명하고 정확하게 FAQ 내용을 작성하세요. 작성한 내용을 다시 한번 검토해보세요. 홈페이지를 처음 방문한 사람이 FAQ에 실린 내용을 읽고 충분히 이해할 수 있을지 생각해보면 도움이 됩니다. 필요하다면 내용을 수정하거나 보충하세요. 사진이나 동영상과 같은 시각 자료를 활용하면 한층 뚜렷한 인상을 남길 수 있습니다.'
    },
    {
        title: '목록에 없는 게임 가이드도 받아볼 수 있나요?',
        subtitle: 'This is FAQ subtitle',
        content: 'FAQ 답변 공간입니다. 분명하고 정확하게 FAQ 내용을 작성하세요. 작성한 내용을 다시 한번 검토해보세요. 홈페이지를 처음 방문한 사람이 FAQ에 실린 내용을 읽고 충분히 이해할 수 있을지 생각해보면 도움이 됩니다. 필요하다면 내용을 수정하거나 보충하세요. 사진이나 동영상과 같은 시각 자료를 활용하면 한층 뚜렷한 인상을 남길 수 있습니다.'
    },
    {
        title: '게임 초보자인데 처음 해볼 만한 게임을 추천해줄 수 있나요?',
        subtitle: 'This is FAQ subtitle',
        content: 'FAQ 답변 공간입니다. 분명하고 정확하게 FAQ 내용을 작성하세요. 작성한 내용을 다시 한번 검토해보세요. 홈페이지를 처음 방문한 사람이 FAQ에 실린 내용을 읽고 충분히 이해할 수 있을지 생각해보면 도움이 됩니다. 필요하다면 내용을 수정하거나 보충하세요. 사진이나 동영상과 같은 시각 자료를 활용하면 한층 뚜렷한 인상을 남길 수 있습니다.'
    }
];

export default function FAQ() {
    let cards = faqs.map(faq => {
        let { title, subtitle, content } = faq;
        return <FAQCard title={title} subtitle={subtitle} content={content} />;
    });

    return (
        <div id="faq">
            <CardDeck>
                {cards}
            </CardDeck>
        </div>
    );
}