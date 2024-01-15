import styled from "styled-components";

const PageContainer = styled.div`
  margin: 20px;
`;

const Header = styled.header`
  display: flex;
  justify-content: space-between;
  align-items: center;
`;

const HeaderActions = styled.div`
  display: flex;
  align-items: center;
  gap: 10px;
`;

const PageContent = styled.div`
  display: grid;
  grid-template-columns: 1fr 3fr 3fr;
  margin-top: 100px;
`;

const TitleInput = styled.input`
  color: rgb(55, 53, 47);
  font-size: 48px;
  margin-bottom: 20px;
  grid-column: 2 / span 2;

  border: none;
  outline: none;
  padding: 5px 0;

  &::placeholder {
    color: rgba(55, 53, 47, 0.4);
    font-size: 48px;
    opacity: 1;
  }
`;

interface ContentInputProps {
  value: string;
}

const ContentInput = styled.input.attrs<ContentInputProps>(props => ({
  size: props.value ? props.value.length : 1,
}))`
  grid-column: 2 / 3;
  font-family: "Space Mono", monospace;
  font-weight: 400;
  font-style: normal;
  display: inline;
  text-align: left;
  border: none;
  outline: none;
  margin-bottom: 20px;

  &::placeholder {
    color: rgba(55, 53, 47, 0.4);
  }
`;

export {
  ContentInput,
  TitleInput,
  PageContent,
  PageContainer,
  HeaderActions,
  Header,
};
