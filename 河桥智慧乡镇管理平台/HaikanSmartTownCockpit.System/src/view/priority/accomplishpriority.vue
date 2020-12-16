<template>
	<div>
		<Card>
			<dz-table :totalCount="stores.role.query.totalCount" :pageSize="stores.role.query.pageSize" @on-page-change="handlePageChanged"
			 @on-page-size-change="handlePageSizeChanged">
				<div slot="searcher">
					<section class="dnc-toolbar-wrap">
						<Row :gutter="16">
							<Col span="16">
							<Form inline @submit.native.prevent>
								<FormItem>
									<Input type="text" search :clearable="true" v-model="stores.role.query.kw" placeholder="请输入重点工作名称" @on-search="handleSearchRole()">
									</Input>
								</FormItem>
								<FormItem>
									<Select v-model="stores.role.query.kwfzr" :clearable="true" style="width: 180px;" placeholder="请选择负责人"
									 filterable>
										<Option v-for="item in fuzerendata" :key="item.systemUserUUID" :value="item.systemUserUUID">{{item.realName}}</Option>
									</Select>
								</FormItem>
								<FormItem>
                    <DatePicker type="date" 
                    style="width: 180px;"
                    :value="stores.role.query.kwendtime" 
                    @on-change="stores.role.query.kwendtime=$event" 
                    format="yyyy-MM-dd"
                    placeholder="开始时间" >
                    </DatePicker>
					</FormItem>
								<FormItem>
                    <DatePicker type="date" 
                    style="width: 180px;"
                    :value="stores.role.query.kwendtime2" 
                    @on-change="stores.role.query.kwendtime2=$event" 
                    format="yyyy-MM-dd"
                    placeholder="结束时间">
                    </DatePicker>
                  </FormItem>
                  <FormItem>
                   <Button
                    v-can="'search'"
                    icon="md-search"
                    type="primary"
                    @click="handleSearchRole()"
                    title="查询">查询
                  </Button>                   
                  </FormItem>
							</Form>
							</Col>
							<Col span="8" class="dnc-toolbar-btns">
							<!-- <Button
                    class="txt-danger"
                    icon="md-trash"
                    title="删除"
                    @click="handleBatchCommand('delete')"
                  ></Button> -->
							<ButtonGroup class="mr3">
								<Button icon="md-refresh" title="刷新" @click="handleRefresh"></Button>
							</ButtonGroup>
							</Col>
						</Row>
					</section>
				</div>

				<Table slot="table" ref="tables" :border="false" size="small" :highlight-row="true" :data="stores.role.data"
				 :columns="stores.columns" @on-selection-change="handleSelectionChange" @on-refresh="handleRefresh" @on-page-change="handlePageChanged"
				 @on-page-size-change="handlePageSizeChanged">
					<template slot-scope="{row,index}" slot="auditStatus">
						<span>{{getauditStatus(row.auditStatus)}}</span>
					</template>
					<template slot-scope="{ row, index }" slot="action">
						<Tooltip placement="top" content="重启"" :delay=" 1000" :transfer="true">
							<Button v-can="'restart'" type="error" size="small" shape="circle" icon="ios-construct" @click="handleDelete(row)"></Button>
						</Tooltip>
						<Tooltip placement="top" content="详情" :delay="1000" :transfer="true">
							<Button v-can="'show'" type="success" size="small" shape="circle" icon="md-search" @click="handleshow(row)"></Button>
						</Tooltip>
					</template>
				</Table>

			</dz-table>
		</Card>
		<!--详情-->
		<Drawer title="详情" v-model="formMode2.opened" width="400" :mask-closable="false" :mask="false" :styles="styles">
			<Form :model="formMode2.fields" ref="详情" label-position="left">
				<FormItem label="标题">
					<Input v-model="formMode2.fields.priorityHeadline" :readonly="true" />
				</FormItem>
				<FormItem label="负责人">
					<Input v-model="formMode2.fields.principalname" :readonly="true" />
				</FormItem>
				<FormItem label="描述" label-position="top">
					<Input type="textarea" v-model="formMode2.fields.priorityDescribe" :rows="4" :readonly="true" />
				</FormItem>
				<FormItem label="参与人">
					<Input v-model="formMode2.fields.participant" :readonly="true" />
				</FormItem>
				<FormItem label="备注">
					<Input v-model="formMode2.fields.remark" :readonly="true" />
				</FormItem>
				<FormItem label="排序字段">
					<Input v-model="formMode2.fields.sortord" :readonly="true" />
				</FormItem>
				<FormItem label="创建时间">
					<Input v-model="formMode2.fields.establishTime" :readonly="true" />
				</FormItem>
				<FormItem label="创建人">
					<Input v-model="formMode2.fields.establishName" :readonly="true" />
				</FormItem>
			</Form>
		</Drawer>
	</div>
</template>

<script>
	import DzTable from "_c/tables/dz-table.vue";
	import config from '@/config';
	import {
		accomplishListRole,
		createRole,
		loadRole,
		editRole,
		priorityRestartRole,
		batchCommand,
		RegistPicture,
		getfuzeren //下拉框
	} from "@/api/EmphasisWork/PriorityWork";

	export default {
		name: "rbac_role_page",
		components: {
			DzTable
		},
		data() {
			return {
				commands: {
					delete: {
						name: "delete",
						title: "删除"
					},
					recover: {
						name: "recover",
						title: "恢复"
					},
					forbidden: {
						name: "forbidden",
						title: "禁用"
					},
					normal: {
						name: "normal",
						title: "启用"
					}
				},
				//编辑
				formModel: {
					opened: false,
					title: "创建重点工作",
					mode: "create",
					selection: [],
					fields: {
						id: "",
						priorityHeadline: "",
					},
					rules: {
						priorityHeadline: [{
							type: "string",
							required: true,
							message: "请输入重点工作标题",
							min: 2
						}]
					}
				},
				//详情
				formMode2: {
					opened: false,
					title: "重点工作详情",
					mode: "show",
					selection: [],
					fields: {
						id: "",
					},
				},
				fuzerendata: [], //负责人

				stores: {
					role: {
						query: {
							totalCount: 0,
							pageSize: 20,
							currentPage: 1,
							kw: "",
							kwfzr: "",
							kwendtime: "",
							kwendtime2:"",
							isDeleted: 0,
							status: -1,
							sort: [{
								direct: "DESC",
								field: "ID"
							}]
						},
						sources: {
							isDeletedSources: [{
									value: -1,
									text: "全部"
								},
								{
									value: 0,
									text: "正常"
								},
								{
									value: 1,
									text: "已删"
								}
							],
							statusSources: [{
									value: -1,
									text: "全部"
								},
								{
									value: 0,
									text: "禁用"
								},
								{
									value: 1,
									text: "正常"
								}
							],
							statusFormSources: [{
									value: 0,
									text: "禁用"
								},
								{
									value: 1,
									text: "正常"
								}
							]
						},
						data: []
					},
					//绑定数据
					columns: [{
							type: "selection",
							width: 50,
							key: "handle"
						},
						{
							title: "名称",
							key: "priorityHeadline",
							ellipsis: true,
							tooltip: true
						},
						{
							title: "负责人",
							ellipsis: true,
							tooltip: true,
							key: "princial"
						},
						{
							title: "完成进度",
							key: "unfinished"
						},
						{
							title: "创建时间",
							key: "establishTime"
						},
						{
							title: "操作",
							align: "center",
							key: "handle",
							width: 150,
							className: "table-command-column",
							slot: "action"
						}
					],
				},
				styles: {
					height: "calc(100% - 55px)",
					overflow: "auto",
					paddingBottom: "53px",
					position: "static"
				}
			};
		},
		computed: {
			formTitle() {
				if (this.formModel.mode === "create") {
					return "创建重点工作";
				}
				if (this.formModel.mode === "edit") {
					return "编辑重点工作";
				}
				return "";
			},
			selectedRows() {
				return this.formModel.selection;
			},
			selectedRowsId() {
				return this.formModel.selection.map(x => x.priorityUuid);
			}
		},
		methods: {

			//负责人下拉框
			loadfuzeren() {
				getfuzeren().then(res => {
					this.fuzerendata = res.data.data
				});
			},

			//查询数据列表
			loadRoleList() {
				accomplishListRole(this.stores.role.query).then(res => {
					this.stores.role.data = res.data.data;
					this.stores.role.query.totalCount = res.data.totalCount;
				});
			},

			//详情重点工作页面
			handleshow(params) {
				this.handleSwitchFormModeToShow();
				// this.handleResetFormRole();
				//this.doLoadShow(params.priorityUuid);
                loadRole({id: params.id}).then(res => {
					this.formMode2.fields = res.data.data;
				});
			},
			//详情重点工作页面
			handleSwitchFormModeToShow() {
				this.formMode2.mode = "show";
				this.handleOpenFormShow();
			},
			//打开详情重点工作右侧导航
			handleOpenFormShow() {
				this.formMode2.opened = true;
			},
			//详情绑定数据
			//doLoadShow(priorityUuid) {
			//	loadRole({
			//		guid: priorityUuid
			//	}).then(res => {
			//		this.formMode2.fields = res.data.data;
			//	});
			//},

			handleSelect(selection, row) {},
			handleSelectionChange(selection) {
				this.formModel.selection = selection;
			},
			//刷新
			handleRefresh() {
				this.loadRoleList();
			},
			handleResetFormRole() {
				this.$refs["formRole"].resetFields();
			},
			handleDelete(params) {
				this.$Modal.confirm({
					title: "操作提示",
					content: "<p>此操作将重启该工作,是否继续?</p>",
					loading: true,
					onOk: () => {
						this.doDelete(params.priorityUuid);
						this.$Modal.remove(); //关闭提示框

					}
				});
			},
			doDelete(ids) {
				if (!ids) {
					this.$Message.warning("请选择至少一条数据");
					return;
				}
				priorityRestartRole(ids).then(res => {
					if (res.data.code === 200) {
						this.$Message.success(res.data.message);
						this.loadRoleList();
					} else {
						this.$Message.warning(res.data.message);
					}
				});
			},
			//批量操作
			handleBatchCommand(command) {
				if (!this.selectedRowsId || this.selectedRowsId.length <= 0) {
					this.$Message.warning("请选择至少一条数据");
					return;
				}
				this.$Modal.confirm({
					title: "操作提示",
					content: "<p>确定要执行当前 [" +
						this.commands[command].title +
						"] 操作吗?</p>",
					loading: true,
					onOk: () => {
						this.doBatchCommand(command);
					}
				});
			},
			//批量操作
			doBatchCommand(command) {
				batchCommand({
					command: command,
					ids: this.selectedRowsId.join(",")
				}).then(res => {
					if (res.data.code === 200) {
						this.$Message.success(res.data.message);
						this.loadRoleList();
						this.formModel.selection = [];
					} else {
						this.$Message.warning(res.data.message);
					}
					this.$Modal.remove();
				});
			},
			handleSearchRole() {
				this.loadRoleList();
			},
			rowClsRender(row, index) {
				if (row.isDeleted) {
					return "table-row-disabled";
				}
				return "";
			},
			//分页
			handlePageChanged(page) {
				this.stores.role.query.currentPage = page;
				this.loadRoleList();
			},
			handlePageSizeChanged(pageSize) {
				this.stores.role.query.pageSize = pageSize;
				this.loadRoleList();
			}
		},
		mounted() {
			this.loadRoleList();
			this.loadfuzeren(); //负责人下拉框
		}
	};
</script>
