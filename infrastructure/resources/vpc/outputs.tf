output "aws_security_group_allow_all_id" {
  value = aws_security_group.allow_all.id
}

output "aws_security_group_vpn_access_id" {
  value = aws_security_group.vpn_access.id
}

output "aws_subnet_main_id" {
  value = aws_subnet.main.id
}

output "aws_subnet_second_id" {
  value = aws_subnet.second.id
}

output "aws_route_table_id" {
  value = aws_route_table.main.id
}

output "aws_vpc_cidr_block" {
  value = aws_vpc.main.cidr_block
}