import React, { FC } from 'react'
import { Container, Row } from 'reactstrap'
// used for making the prop types of this component
import PropTypes from 'prop-types'

type FooterProps = {
  default?: boolean
  fluid?: boolean
}

const Footer: FC<FooterProps> = (props: FooterProps) => {
  return (
    <footer className={'footer' + (props.default ? ' footer-default' : '')}>
      <Container fluid={props.fluid ? true : false}>
        <Row>
          <nav className="footer-nav">
            <ul>
              <li>
                <a href="https://www.creative-tim.com" target="_blank">
                  Creative Tim
                </a>
              </li>
              <li>
                <a href="https://blog.creative-tim.com" target="_blank">
                  Blog
                </a>
              </li>
              <li>
                <a href="https://www.creative-tim.com/license" target="_blank">
                  Licenses
                </a>
              </li>
            </ul>
          </nav>
          <div className="credits ml-auto">
            <div className="copyright">
              &copy; {1900 + new Date().getFullYear()}, made with{' '}
              <i className="fa fa-heart heart" /> by Creative Tim
            </div>
          </div>
        </Row>
      </Container>
    </footer>
  )
}

export default Footer
