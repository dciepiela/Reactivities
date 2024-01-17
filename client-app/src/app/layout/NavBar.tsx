import {
  Button,
  Container,
  Dropdown,
  DropdownItem,
  DropdownMenu,
  Image,
  Menu,
  MenuItem,
} from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import { Link, NavLink } from "react-router-dom";
import { useStore } from "../stores/store";

export default observer(function NavBar() {
  const {
    userStore: { user, logout },
  } = useStore();
  return (
    <Menu inverted fixed="top">
      <Container>
        <MenuItem as={NavLink} to="/" header>
          <img
            src="/assets/logo.jpg"
            alt="logo"
            style={{ marginRight: "20px" }}
          />
          Reactivities
        </MenuItem>
        <MenuItem name="Activities" as={NavLink} to="/activities" />
        <MenuItem name="Errors" as={NavLink} to="/errors" />
        <MenuItem>
          <Button
            as={NavLink}
            to="/createActivity"
            positive
            content="Create Activity"
          />
        </MenuItem>
        <MenuItem position="right">
          <Image
            src={user?.image || "./assets/user.png"}
            avatar
            spaced="right"
          />
          <Dropdown pointing="top left" text={user?.displayName}>
            <DropdownMenu>
              <DropdownItem
                as={Link}
                to={`profile/${user?.userName}`}
                text="My Profile"
                icon="user"
              />
              <DropdownItem onClick={logout} text="Logout" icon="power" />
            </DropdownMenu>
          </Dropdown>
        </MenuItem>
      </Container>
    </Menu>
  );
});
