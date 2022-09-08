terraform {
  required_version = ">= 0.12"
}

provider "aws" {
  region = "eu-west-2"
}

module "acm" {
  source = "../acm"
}

module "vpc" {
  source = "../vpc"
}

resource "aws_ec2_client_vpn_endpoint" "vpn" {
  client_cidr_block = "10.20.0.0/22"
  split_tunnel = true
  server_certificate_arn = module.acm.aws_acm_certificate_validation_vpn_server_arn

  authentication_options {
    type = "certificate-authentication"
    root_certificate_chain_arn = module.acm.aws_acm_certificate_vpn_client_root_arn
  }

  connection_log_options {
    enabled = false
  }
}

resource "aws_ec2_client_vpn_network_association" "vpn_subnets" {
  count = length(aws_subnet.sn_az)
  client_vpn_endpoint_id = aws_ec2_client_vpn_endpoint.vpn.id
  subnet_id = aws_subnet.sn_az[count.index].id
  security_groups = [aws_security_group.vpn_access.id]

  lifecycle {
    ignore_changes = [subnet_id]
  }
}

resource "aws_ec2_client_vpn_authorization_rule" "vpn_auth_rule" {
  client_vpn_endpoint_id = aws_ec2_client_vpn_endpoint.vpn.id
  target_network_cidr = aws_vpc.main.cidr_block
  authorize_all_groups = true
}