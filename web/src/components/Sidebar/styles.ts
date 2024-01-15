import styled from "styled-components";

const Container = styled.div`
  width: 260px;
  background-color: rgba(52, 53, 65);
  padding: 20px 30px;
`;

const SContent = styled.div`
  display: flex;
  flex-direction: column;
  color: white;
  height: 100%;
  justify-content: space-between;
`;

const StatisticContainer = styled.div`
  margin-top: 20px;
`;

const SLinks = styled.div`
  margin-top: 30px;
  display: flex;
  flex-direction: column;
  gap: 15px;
`;

const SBottomLinks = styled.div`
  border-top: 1px solid white;
  display: flex;
  flex-direction: column;
  margin-top: auto;
`;

const SAuthLinks = styled.div`
  display: flex;
  flex-direction: row;
`;

export {
  Container,
  SContent,
  SLinks,
  SBottomLinks,
  SAuthLinks,
  StatisticContainer,
};
