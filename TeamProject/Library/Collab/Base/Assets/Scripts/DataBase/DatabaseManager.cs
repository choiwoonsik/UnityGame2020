/**
 * @brief 데이터베이스모음/관리
 * @date 2020/03/05 마지막수정
 * @file DatabaseManager.cs
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{

    public static List<News> newsDataList; /* 뉴스 거리 데이터베이스*/
    public static List<SimpleNote> buildingConditionNoteList; /* 건물이 더러워졌을 때 주민들이 보내는 쪽지 데이터베이스 */

    //사다리
    public static string sadariGameItem;

    //사다리게임 결과 받아서 데이타베이스에 저장
    public void GetItemResult(string item)
    {
        sadariGameItem = item;
    }

    // Start is called before the first frame update
    void Start()
    {
        newsDataList = new List<News>();
        InitNewsData();
        buildingConditionNoteList = new List<SimpleNote>();
        InitBuildingConditionNoteData();
    }

    /* 건물이 더러워졌을 때 주민들이 보내는 쪽지 */
    private void InitBuildingConditionNoteData()
    {
        buildingConditionNoteList.Add(new SimpleNote("아니 건물이 왜이렇게 드러워요? 관리를 하는 거야 안하는거야 나 나갈 테니까 그런줄 아쇼!"));
        buildingConditionNoteList.Add(new SimpleNote("우웩, 건물 화장실에 거미가 있어요. 관리비를 내는 이유를 모르겠는데, 나 이번 달 안낼래!"));
        buildingConditionNoteList.Add(new SimpleNote("엘레베이터에서 냄새가 나서 못살겠어요. 나 나갈래."));
        buildingConditionNoteList.Add(new SimpleNote("건물이 너무 낡아서 쓰러질거 같아요. 어떻게 그러면서 월세를 이만큼을 받아? 양심이 있는 거야 없는거야."));
        buildingConditionNoteList.Add(new SimpleNote("건물 페인트 칠 좀 다시 해주시면 안되요?? 어우 짜증나."));
        buildingConditionNoteList.Add(new SimpleNote("건물 쓰레기통좀 비워주세요. 쓰레기 냄새가 너무 나서..."));
        buildingConditionNoteList.Add(new SimpleNote("건물이 너무 낡았어요. 수준 떨어져서 못 살겠네 정말"));
        buildingConditionNoteList.Add(new SimpleNote("관리자 맞으시죠? 건물 보수 좀 부탁드려요. 안 그러면 저 다음 달에 나갈 거에요!"));
        buildingConditionNoteList.Add(new SimpleNote("건물 한번만 싹 청소 좀 해주세요. 안 그러면 저 다음 달에 나갈 거에요!"));
        buildingConditionNoteList.Add(new SimpleNote("으악 ! 이 빌딩에 더 이상 못살겠어요. 리모델링 좀 해주세요. 안 그러면 저 다음 달에 나갈 거에요!"));
        buildingConditionNoteList.Add(new SimpleNote("미안하다, 이건물 보여줄려고 어그로끌었다. 사람사는 건물 수준 실화냐?"));
        buildingConditionNoteList.Add(new SimpleNote("이 집의 장점은 환풍이에요^^ 문을 다닫아도 바람이 다들어와~"));
        buildingConditionNoteList.Add(new SimpleNote("이 동네는 주민들끼리 소통이 참 잘되더라구요! 밑집에서 하는 얘기가 다들려! 방음따윈 개나줘버린거야~!"));
        buildingConditionNoteList.Add(new SimpleNote("우리 곤충원가요 엄마!! 아... 아니다 여기가 곤충원이구나^^"));
    }

    private void InitNewsData()
    {
        /*신제품 출시*/
        newsDataList.Add(new News(100, "그냥신문", "A기업의 에이폰이 최근 양산해 돌입해 이르면 몇 주 내 출시될 것이라고 IT매체 Z가 22일(현지시간) A기업 내부 소식통을 인용해 보도했다", 0, 0.3f, 20));
        newsDataList.Add(new News(100, "그냥신문", "A기업이 11일(현지시간) 미국 샌프란시스코에서 열린 ‘IT 2020’에서 명품 패션 브랜드 C와 협업한 프리미엄 패키지 ‘에이폰 C 에디션’을 공개했다. ", 0, 0.2f, 20));
        newsDataList.Add(new News(100, "그냥신문", "A기업이 ‘전략적 팀 전투(TFT)’ 모바일 버전을 20일 글로벌 시장에 동시 출시한다고 18일 밝혔다", 0, 0.3f, 20));
        /*신기능 출시*/
        newsDataList.Add(new News(100, "그냥신문", "A기업 서비스 에이그램에서 4일(현지 시간) 인공 지능을 이용한 신기능을 추가했다고 발표했다. 이 기능은 에이그램의 스마트폰용 최신 버전을 설치하면 이용할 수 있다.", 0, 0.2f, 30));
        newsDataList.Add(new News(100, "그냥신문", "사진, 동영상 공유 플랫폼인 A기업의 에이그램은 보다 재미있는 콘텐츠 작성을 위한 신기능을 내놨다고 17일 밝혔다.", 0, 0.2f, 20));
        newsDataList.Add(new News(100, "그냥신문", "A기업이 15일(현지시간) 미국 뉴욕에서 레이더로 얼굴을 인식하고 밤하늘 별까지 촬영이 가능한 기능을 공개했다.", 0, 0.3f, 30));
        newsDataList.Add(new News(100, "그냥신문", "A기업이 24일 에이게임에 대규모 업데이트를 진행했다. 이번에 적용되는 패치는 오픈 베타 테스트 기간 동안 축적된 데이터와 이용자의 의견을 적극 반영한 것으로 보인다.", 0, 0.15f, 30));
        /* 해외 대기업과 계약 체결 */
        newsDataList.Add(new News(100, "그냥신문", "A기업의 대표 휴양 레저시설인 ‘에이필드’가 10일(현지시간) 베트남의 리조트 회사인 D와 마스터 프랜차이즈 계약을 맺고, 오는 2020년 베트남의 대표적 휴양지인 나트랑에 에이필드를 선보이기로 했다고 밝혔다.", 0, 0.3f, 30));
        newsDataList.Add(new News(100, "그냥신문", "A기업의 스마트미디어 솔루션업체 에이드가 지난해 11월에 이어 후속투자 유치에 성공했다. 20일 업계에 따르면 에이드는 최근 20억원 규모의 시리즈A 투자를 유치했다. 에이드는 이번 투자 유치를 기반으로 해외 진출에 속도를 낼 전망이다.", 0, 0.3f, 30));
        /* 미디어를 통한 긍정적 이미지 생성 및 홍보 효과 */
        newsDataList.Add(new News(100, "그냥신문", "A그룹의 에이크가 이번 S/S시즌 패션 크리에이터이자 인플루언서 정미미와 협업한다. 에이크는 정미미와 컬래버레이션을 통해 다양한 멀티 라이프스타일 패션을 선보이겠다고 전한다.", 0, 0.15f, 30));

        /*주식 떨어지는 내용 */
        /*부하 직원 하대*/
        newsDataList.Add(new News(100, "그냥신문", "A기업 부사장이 지난 5일(현지시간) 미국 JFK 국제공항에서 인천으로 출발하는 대한항공 KE086편 항공기를 '램프리턴(승객의 안전에 문제가 생겼을 경우 취하는 조치)' 했다.", 0, -0.3f, -30));
        newsDataList.Add(new News(100, "그냥신문", "A기업 전무가 광고 대행업체와의 회의 자리에서 대행사 직원에게 물을 뿌렸다는 의혹", 0, -0.1f, -30));
        newsDataList.Add(new News(100, "그냥신문", "수행기사들에게 불법 운전을 지시한 강요죄와 전문의약품인 ‘센돔’을 지인들에게 양도한 약사법 위반 등의 혐의로 이 회장을 불구속 기소 의견으로 검찰에 송치했다", 0, -0.20f, -30));
        newsDataList.Add(new News(100, "그냥신문", "A그룹 회장의 셋째 아들이 술집 종업원을 폭행하는 사건이 일어났습니다.", 0, -0.05f, -30));
        newsDataList.Add(new News(100, "그냥신문", "한국 산재협의회에 따르면 A기업에서 근무한 뒤 병을 얻어 사망에 이르는 산업재해가 지속적으로 발생하고 있다고 한다. 산업안전보건연구원이 이 의혹에 대해 역학조사를 실시한다.", 0, -0.05f, -30));
        /*마약*/
        newsDataList.Add(new News(100, "그냥신문", "A그룹 창업주의 3세에게 마약을 제공했다고 한 이모(30)씨가 2일 오후 9시쯤 마약수사대 사무실로 자진출석 했다고 밝혔다.", 0, -0.1f, -30));
        newsDataList.Add(new News(100, "그냥신문", "액상 대마를 밀수해 흡연한 혐의로 A그룹 회장의 차남인 허모 전 부사장이 징역 3년과 집행유예 4년을 선고받았다.", 0, -0.15f, -30));
        /*비리*/
        newsDataList.Add(new News(100, "그냥신문", "A그룹 노조가 그간 암암리에 있었던 인사 청탁 문제를 폭로하겠다는 입장을 내놨습니다.", 0, -0.05f, -30));
        newsDataList.Add(new News(100, "그냥신문", "뇌물수수와 횡령 등의 혐의로 재판에 넘겨진 A그룹 대표, 검찰이 20년 구형했다.", 0, -0.3f, -40));
        newsDataList.Add(new News(100, "그냥신문", "서울지방국세청이 조세포탈 혐의로 조 회장을 검찰에 고발하면서 시작됐다. 고발장을 접수한 서울남부지검은 기업·금융범죄전담 부인 형사6부에 사건을 배당해 조사에 본격 착수했다. 검찰은 지난해 6월 조 회장을 피의자 신분으로 처음 소환해 약 16시간에 걸친 고강도 조사를 벌인 뒤 7월초 구속영장을 청구했다.", 0, -0.25f, -40));
        /*CEO의 사망*/
        newsDataList.Add(new News(100, "그냥신문", "A그룹 발표에 따르면 조 회장이 이날 새벽 미국 현지에서 폐질환으로 별세했다. ", 0, -0.1f, -40));
        newsDataList.Add(new News(100, "그냥신문", "A그룹 전 CEO, A씨가 사망했다고 A기업이 공식발표했다. A씨는 A그룹의 핵심 인물이며, 몇 년째 췌장암으로 투병해왔다.", 0, -0.5f, -40));
        /*불량품 발생*/
        newsDataList.Add(new News(100, "그냥신문", "국토교통부는 A,B,C기업에서 수입,판매한 총 37개 차종 2만 285대에서 제작 결함이 발견돼 시정조치(리콜)한다고 밝혔다.", 0, -0.1f, -40));
        newsDataList.Add(new News(100, "그냥신문", "A회사의 스마트폰 발화요인에 대해 정부역시 A사에서 이미 발표한 것처럼 배터리에 문제가 있었던 것으로 파악했다. 이로 인해 A회사는 판매중지 및 리콜을 시행한다.", 0, -0.05f, -40));
        /*개인정보 유출*/
        newsDataList.Add(new News(100, "그냥신문", "A회사의 SNS 이용자 약 2억 6천 7백만명의 개인정보가 유출됐다고 AP와 로이터 통신 등이 보도했습니다.", 0, -0.15f, -40));
        newsDataList.Add(new News(100, "그냥신문", "A회사의 온라인게임 회원 1300만명의 개인정보가 유출됐다.", 0, -0.2f, -50));
        /*기밀 유출*/
        newsDataList.Add(new News(100, "운식신문", "A그룹 기밀부서인 미래전략기획실에서 극비리에 개발중인 프로젝트의 핵심 기술을 조모 씨 외 2명의 일당이 중국으로 유출한것이 드러났습니다.", 0, -0.25f, -50));
        newsDataList.Add(new News(100, "운식신문", "검찰은 A그룹의 신제품 개발 부서인 4차산업 전략기획실에서 야심차게 개발중인 프로젝트의 핵심 기술을 야모 씨 외 2명의 일당이 일본으로 유출한 정황을 파악했다고 발표했습니다.", 0, -0.25f, -50));
        newsDataList.Add(new News(100, "운식신문", "검찰 고위 관계자에 따르면 A그룹의 신제품 개발 부서내 플래그쉽전략실에서 공들여 개발중이였던 프로젝트의 핵심 부품을 퍄모 씨 등 3명의 일당이 경쟁기업에 팔아넘기는 행위를 파악하여 구속하였다고 보도했습니다.", 0, -0.3f, -50));
        /*탈세*/
        newsDataList.Add(new News(100, "운식신문", "A그룹 고위 관계자에 따르면 A그룹의 법률 자문 위원회에서 탈세법을 위반한 혐의로 수사중에 있다고 전해왔습니다.", 0, -0.3f, -40));
        newsDataList.Add(new News(100, "운식신문", "A그룹 고위 관계자로부터 A그룹내 후계자 계승에 따른 상속세에 대한 탈세를 목적으로 주가 조작행위를 조직적으로 벌인 행위에 대한 폭로를 할것이라는 공익 제보가 있었습니다.", 0, -0.3f, -40));
        newsDataList.Add(new News(100, "운식신문", "검찰 내부 소식통에 의하면 A그룹내에서 대규모의 조직적 탈세행위를 포착하여 수사에 착수한다고 알려왔습니다.", 0, -0.35f, -40));
        /*손님무시*/
        newsDataList.Add(new News(100, "운식신문", "A기업의 00매장에서 고객의 신체적특징을 비하하고 희화화하는 행위가 발각되어 소비자들의 불매운동 조짐이 나타나고 있습니다.", 0, -0.2f, -30));
        newsDataList.Add(new News(100, "운식신문", "A기업의 00에 위치한 00매장에서 고객의 외모특징을 특정 동물에 빗대어 비하한 행위를 알바생이 폭로하여 소비자들의 불매운동 조짐이 나타나고 있습니다.", 0, -0.2f, -30));
        newsDataList.Add(new News(100, "운식신문", "00매장에서 10년간 일해온 김모씨가 고객들의 적립 포인트를 빼돌려 자신의 적립카드에 적립한 행위가 발각되었지만 개인의 일탈이라며 방관하는 회사의 무책임한 태도에 소비자들의 비난 여론이 확산중에 있습니다.", 0, -0.2f, -30));
        newsDataList.Add(new News(100, "운식신문", "A기업에서 새로 출시한 제품에 내부적 결함을 출시전에 발견하였음에도 불구하고 소비자들은 다 개돼지다라며 출시를 강행한 사실이 발각되어 검찰이 수사에 착수 하였습니다.", 0, -0.3f, -40));
        newsDataList.Add(new News(100, "운식신문", "A기업의 통조림 제품에서 벌레가 나왔음에도 소비자 과실이라며 방관적인 태도를 취하였다는 제보를 받았습니다.", 0, -0.3f, -30));
        /*사고*/
        newsDataList.Add(new News(100, "운식신문", "베트남에 위치한 A기업의 생산공장에서 도적떼의 습격을 받아 출하 대기중이던 5천여개의 제품을 도난 당하였다고 발표했습니다.", 0, -0.1f, -30));
        newsDataList.Add(new News(100, "운식신문", "미국 플로리다주에 위치한 A기업의 생산공장에 태풍급 허리케인의 영향으로 인해 생산 중단에 들어갔다고 발표하였습니다.", 0, -0.1f, -30));
        newsDataList.Add(new News(100, "운식신문", "강원도 인제군에 위치한 A기업의 생산공장에서 북한의 남파공작원의 적대행위로 인해 정전이 일어나 생산량에 차질이 생겼다고 발표했습니다.", 0, -0.1f, -30));
        newsDataList.Add(new News(100, "운식신문", "경기도 안양시에 위치한 A기업의 데이터센터가 폭우로 침수가 발생하여 데이터 복구작업에 착수중이라고 발표하였습니다.", 0, -0.2f, -30));
        newsDataList.Add(new News(100, "운식신문", "대규모 산불의 영향으로 A기업의 생산공장이 화재로 모두 불타 버렸다는 소식입니다.", 0, -0.3f, -30));
        newsDataList.Add(new News(100, "운식신문", "전지구적으로 바이러스가 창궐하여 사회적 거리두기가 시행되어 A기업이 상영관 절반을 닫는다고 발표하였습니다.", 0, -0.3f, -30));
        /*리콜*/
        newsDataList.Add(new News(100, "운식신문", "A기업에서 당해년도에 출하한 제품에 심각한 기계적 결함이 있음을 인정하고 출하를 앞둔 약 300만대에 이르는 제품을 모두 리콜하고 판매완료된 상품에 대해선 보상한다고 발표했습니다.", 0, -0.3f, -30));
        newsDataList.Add(new News(100, "운식신문", "A기업에서 전해년도에 출하한 제품에 심각한 소프트웨어결함이 있음을 인정하고 출하를 앞둔 약 100만대에 이르는 제품을 모두 리콜하고 판매완료된 상품에 대해선 보상한다고 발표했습니다.", 0, -0.3f, -30));
        newsDataList.Add(new News(100, "운식신문", "A기업에서 지난 5년간 출하한 제품에 대해 심각한 내부적결함이 있음을 인정하고 약 100만대에 이르는 출하전 제품을 모두 리콜하고 판매제품 소지후 방문시 교체를 진행한다고 발표했습니다.", 0, -0.3f, -30));
        /*사회적 이슈*/
        newsDataList.Add(new News(100, "운식신문", "A기업의 사외이사 조모씨가 회식자리에서 여사원에게 접대를 강요했다는 제보가 들어와 경찰이 수사에 착수했습니다.", 0, -0.1f, -30));
        newsDataList.Add(new News(100, "운식신문", "A기업 회장 강모씨의 셋째 아들이 성매매 이슈에 휩싸여 논란이 일고있습니다.", 0, -0.2f, -30));
        newsDataList.Add(new News(100, "운식신문", "A기업 부회장이 차량 전용 기사에게 상습적인 성추행 및 욕설을 일삼와 왔다는 사실에 많은 사람들이 분개하고있습니다.", 0, -0.3f, -30));
        newsDataList.Add(new News(100, "운식신문", "A기업 고위 임원의 둘째 아들이 음주운전을 하고난 후 운전자 바꿔치기를 하고 도주했음에도 불구속 처분을 받아 비난여론이 매우 들끓고있습니다.", 0, -0.3f, -30));
        newsDataList.Add(new News(100, "운식신문", "A기업의 고위 임원이 상습적으로 부하직원들에게 성휘롱 및 손찌검을 일삼았다는 제보가 들어와 경찰이 수사에 착수했습니다.", 0, -0.3f, -30));
        //newsDataList.Add(new News(100, "운식신문", ".", 0, -0.3f, -30));

    }
}
