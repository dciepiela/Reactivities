import { Fragment } from "react";
import {
  Button,
  Placeholder,
  PlaceholderLine,
  PlaceholderParagraph,
  Segment,
  SegmentGroup,
} from "semantic-ui-react";

export default function ActivityListItemPlaceholder() {
  return (
    <Fragment>
      <Placeholder fluid style={{ marginTop: 25 }}>
        <SegmentGroup>
          <Segment style={{ minHeight: 110 }}>
            <Placeholder>
              <Placeholder.Header image>
                <PlaceholderLine />
                <PlaceholderLine />
              </Placeholder.Header>
              <PlaceholderParagraph>
                <PlaceholderLine />
              </PlaceholderParagraph>
            </Placeholder>
          </Segment>
          <Segment>
            <Placeholder>
              <PlaceholderLine />
              <PlaceholderLine />
            </Placeholder>
          </Segment>
          <Segment secondary style={{ minHeight: 70 }} />
          <Segment clearing>
            <Button disabled color="blue" floated="right" content="View" />
          </Segment>
        </SegmentGroup>
      </Placeholder>
    </Fragment>
  );
}
