import './introduction.scss';
import intro from './intro.webp';

function Introduction() {
    return (
        <div id="introduction">
            <div className="description">
                <h1>우리 이야기</h1>
                <h3 className="pt-4">진정한 게이머</h3>
                <p className="pt-4">
                    게이머가 정보를 교환할 수 있는 공간을 마련하자는 간단한 이유가 Re;born의 역사를 시작하는 계기가 되었습니다.
                    이제 우리는 최고의 게임 사이트로써 사용자에게 종합적이며 품질이 높은 콘텐츠를 제공하고 있습니다.
                    게임 커뮤니티의 일원으로 최선을 다해 게이머를 지원할 수 있도록 노력하겠습니다.
            </p>
            </div>
            <div className="pt-5">
                <img src={intro} alt="introduction" />
            </div>
        </div>
    );
}

export default Introduction;;;